// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.12.0+8c27801dc8d42ccc00997f25c0b8f45f8d4a233e
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace OrderProcessing.Events
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("avrogen", "1.12.0+8c27801dc8d42ccc00997f25c0b8f45f8d4a233e")]
	public partial class UnavailableItem : global::Avro.Specific.ISpecificRecord
	{
		public static global::Avro.Schema _SCHEMA = global::Avro.Schema.Parse("{\"type\":\"record\",\"name\":\"UnavailableItem\",\"namespace\":\"OrderProcessing.Events\",\"f" +
				"ields\":[{\"name\":\"ProductId\",\"type\":\"string\"},{\"name\":\"RequestedQuantity\",\"type\":" +
				"\"int\"},{\"name\":\"AvailableQuantity\",\"type\":\"int\"}]}");
		private string _ProductId;
		private int _RequestedQuantity;
		private int _AvailableQuantity;
		public virtual global::Avro.Schema Schema
		{
			get
			{
				return UnavailableItem._SCHEMA;
			}
		}
		public string ProductId
		{
			get
			{
				return this._ProductId;
			}
			set
			{
				this._ProductId = value;
			}
		}
		public int RequestedQuantity
		{
			get
			{
				return this._RequestedQuantity;
			}
			set
			{
				this._RequestedQuantity = value;
			}
		}
		public int AvailableQuantity
		{
			get
			{
				return this._AvailableQuantity;
			}
			set
			{
				this._AvailableQuantity = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.ProductId;
			case 1: return this.RequestedQuantity;
			case 2: return this.AvailableQuantity;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.ProductId = (System.String)fieldValue; break;
			case 1: this.RequestedQuantity = (System.Int32)fieldValue; break;
			case 2: this.AvailableQuantity = (System.Int32)fieldValue; break;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
