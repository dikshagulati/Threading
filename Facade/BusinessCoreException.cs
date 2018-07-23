using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace WebApplication1.Facade
{
    public class BusinessCoreExceptionData
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public string StackTrace { get; set; }
    }

    [Serializable]
    public class BusinessCoreException : Exception
    {
        private readonly Type _typeOfAgent;
        private readonly BusinessCoreExceptionData _errorData;

        public BusinessCoreException(Type typeOfAgent, string message, BusinessCoreExceptionData errorData = null)
            : base(message)
        {
            _typeOfAgent = typeOfAgent;
            _errorData = errorData;
        }

        public BusinessCoreException(Type typeOfAgent, string message, Exception innerException, BusinessCoreExceptionData errorData = null)
            : base(message, innerException)
        {
            _typeOfAgent = typeOfAgent;
            _errorData = errorData;
        }


        protected BusinessCoreException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public Type TypeOfAgent
        {
            get { return _typeOfAgent; }
        }

        public BusinessCoreExceptionData ErrorData
        {
            get { return _errorData; }
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("TypeOfAgent", _typeOfAgent);
            info.AddValue("ErrorData", _errorData);

            base.GetObjectData(info, context);
        }
    }
}
