using NetworkCommsDotNet.DPSBase;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MySARAssist.ResourceClasses
{
    /// <summary>
    /// <see cref="DataSerializer"/> using <see href="http://code.google.com/p/protobuf-net/">ProtoBuf-Net</see> to serialize an <see cref="object"/> to bytes
    /// </summary>
    [DataSerializerProcessor(1)]
    public class ProtobufSerializer : DataSerializer
    {
        private static int metaDataTimeoutMS = 150000;

#if ANDROID || iOS
        [Preserve]
#endif
        public ProtobufSerializer() { }

        #region Depreciated

        static DataSerializer instance;

        /// <summary>
        /// Instance singleton used to access <see cref="DataSerializer"/> instance.  Use instead <see cref="DPSManager.GetDataSerializer{T}"/>
        /// </summary>
        [Obsolete("Instance access via class obsolete, use DPSManager.GetDataSerializer<T>")]
        public static DataSerializer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GetInstance<ProtobufSerializer>();

                    //Increase timeout to prevent errors when CPU busy
                    RuntimeTypeModel.Default.MetadataTimeoutMilliseconds = metaDataTimeoutMS;
                }

                return instance;
            }
        }

        #endregion

        #region ISerialize Members

        /// <inheritdoc />
        protected override void SerialiseDataObjectInt(Stream ouputStream, object objectToSerialise, Dictionary<string, string> options)
        {
            ProtoBuf.Serializer.NonGeneric.Serialize(ouputStream, objectToSerialise);
            ouputStream.Seek(0, 0);
        }

        /// <inheritdoc />
        protected override object DeserialiseDataObjectInt(Stream inputStream, Type resultType, Dictionary<string, string> options)
        {
            return ProtoBuf.Serializer.NonGeneric.Deserialize(resultType, inputStream);
        }

        #endregion
    }

}
