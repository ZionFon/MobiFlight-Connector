﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ArcazeUSB;

namespace MobiFlight.InputConfig
{
    public class FsuipcOffsetInputAction : InputAction, ICloneable
    {
        public const int FSUIPCOffsetNull = 0;
        [XmlAttribute]
        public int FSUIPCOffset { get; set; }
        [XmlAttribute]
        public byte FSUIPCSize { get; set; }
        [XmlAttribute]
        public FSUIPCOffsetType FSUIPCOffsetType { get; set; }
        [XmlAttribute]
        public long FSUIPCMask { get; set; }
        [XmlAttribute]
        public double FSUIPCMultiplier { get; set; }
        [XmlAttribute]
        public bool FSUIPCBcdMode { get; set; }
        [XmlAttribute]
        public String InputValue { get; set; }


        public FsuipcOffsetInputAction()
        {
            FSUIPCOffset = FSUIPCOffsetNull;
            FSUIPCMask = 0xFF;
            FSUIPCMultiplier = 1.0;
            FSUIPCOffsetType = FSUIPCOffsetType.Integer;
            FSUIPCSize = 1;
            FSUIPCBcdMode = false;
            InputValue = "";
        }

        override public object Clone()
        {
            FsuipcOffsetInputAction clone = new FsuipcOffsetInputAction();
            clone.FSUIPCOffset = this.FSUIPCOffset;
            clone.FSUIPCOffsetType = this.FSUIPCOffsetType;
            clone.FSUIPCSize = this.FSUIPCSize;
            clone.FSUIPCMask = this.FSUIPCMask;
            clone.FSUIPCMultiplier = this.FSUIPCMultiplier;
            clone.FSUIPCBcdMode = this.FSUIPCBcdMode;
            clone.InputValue = this.InputValue;
            return clone;
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("type", "FsuipcOffsetInputAction");
            writer.WriteStartElement("source");
                writer.WriteAttributeString("type", "FSUIPC");
                writer.WriteAttributeString("offset", "0x" + FSUIPCOffset.ToString("X4"));
                writer.WriteAttributeString("offsetType", FSUIPCOffsetType.ToString());
                writer.WriteAttributeString("size", FSUIPCSize.ToString());
                writer.WriteAttributeString("mask", "0x" + FSUIPCMask.ToString("X4"));
                writer.WriteAttributeString("multiplier", FSUIPCMultiplier.ToString(serializationCulture));
                writer.WriteAttributeString("bcdMode", FSUIPCBcdMode.ToString());
                writer.WriteAttributeString("inputValue", InputValue);
            writer.WriteEndElement();
        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            reader.Read(); // this should be the "source"

            if (reader.LocalName == "source")
            {
                FSUIPCOffset = Int32.Parse(reader["offset"].Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                FSUIPCSize = Byte.Parse(reader["size"]);
                if (reader["offsetType"] != null && reader["offsetType"] != "")
                {
                    try
                    {
                        FSUIPCOffsetType = (FSUIPCOffsetType)Enum.Parse(typeof(FSUIPCOffsetType), reader["offsetType"]);
                    }
                    catch (Exception e)
                    {
                        FSUIPCOffsetType = ArcazeUSB.FSUIPCOffsetType.Integer;
                    }
                }
                else
                {
                    // Backward compatibility
                    // byte 1,2,4 -> int, this already is default
                    // exception
                    // byte 8 -> float
                    if (FSUIPCSize == 8) FSUIPCOffsetType = ArcazeUSB.FSUIPCOffsetType.Float;
                }
                FSUIPCMask = Int64.Parse(reader["mask"].Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                FSUIPCMultiplier = Double.Parse(reader["multiplier"], serializationCulture);
                if (reader["bcdMode"] != null && reader["bcdMode"] != "")
                {
                    FSUIPCBcdMode = Boolean.Parse(reader["bcdMode"]);
                }

                if (reader["inputValue"] != null && reader["inputValue"] != "")
                {
                    InputValue = reader["inputValue"];
                }

                // read to the end not needed
            }
        }
    }
}
