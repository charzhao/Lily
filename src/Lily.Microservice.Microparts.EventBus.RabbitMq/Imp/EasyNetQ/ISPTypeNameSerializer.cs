using System;
using System.Collections.Concurrent;
using System.Reflection;
using EasyNetQ;
using Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.Setting;

namespace Lily.Microservice.Microparts.EventBus.RabbitMq.Imp.EasyNetQ
{

    public class IspTypeNameSerializer : ITypeNameSerializer
    {
        private readonly ConcurrentDictionary<string, Type> _deserializedTypes = new ConcurrentDictionary<string, Type>();

        public Type DeSerialize(string typeName)
        {
            CheckNotBlank(typeName, "typeName");

            return _deserializedTypes.GetOrAdd(typeName, t =>
            {
                var nameParts = t.Split(':');
                if (nameParts.Length != 2)
                {
                    throw new EasyNetQException("type name {0}, is not a valid EasyNetQ type name. Expected Type:Assembly", t);
                }
                var type = Type.GetType(nameParts[0] + ", " + nameParts[1]);
                // if cannot find type and the message from ISP service( contains Honeywell.ISP.Services.Common.Entities)
                // try to find in stub assembly
                if (type == null && nameParts[0].Contains("Honeywell.ISP.Services.Common.Entities"))
                {
                    // if exist StubAssembly, find in Stub Assembly
                    if (!string.IsNullOrEmpty(MessagingConfiguration.Instance.IspEventStubAssemblyName))
                    {
                        type = Type.GetType(nameParts[0] + ", " + MessagingConfiguration.Instance.IspEventStubAssemblyName);
                    }
                    if (type == null)
                    {
                        type = Type.GetType(MessagingConfiguration.Instance.IspEventDefaultTypeFullName);
                        if (type == null)
                        {
                            throw new EasyNetQException("Cannot find type {0}", t);
                        }
                    }
                }
                return type;
            });
        }

        private readonly ConcurrentDictionary<Type, string> _serializedTypes = new ConcurrentDictionary<Type, string>();

        public string Serialize(Type type)
        {
            CheckNotNull(type, "type");

            return _serializedTypes.GetOrAdd(type, t =>
            {

                var typeName = t.FullName + ":" + t.GetTypeInfo().Assembly.GetName().Name;
                if (typeName.Length > 255)
                {
                    throw new EasyNetQException("The serialized name of type '{0}' exceeds the AMQP " +
                                                "maximum short string length of 255 characters.", t.Name);
                }
                return typeName;
            });
        }


        public static void CheckNotBlank(string value, string name)
        {
            CheckNotBlank(value, name, string.Format("{0} must not be blank", name));
        }

        public static void CheckNotNull<T>(T value, string name) where T : class
        {
            CheckNotNull(value, name, string.Format("{0} must not be null", name));
        }

        public static void CheckNotBlank(string value, string name, string message)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name must not be blank", "name");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message must not be blank", "message");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message, name);
            }
        }

        public static void CheckNotNull<T>(T value, string name, string message) where T : class
        {
            if (value == null)
            {
                CheckNotBlank(name, "name", "name must not be blank");
                CheckNotBlank(message, "message", "message must not be blank");

                throw new ArgumentNullException(name, message);
            }
        }
    }
}
