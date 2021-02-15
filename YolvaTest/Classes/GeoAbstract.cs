using System;
using System.Collections.Generic;
using System.Text;

namespace GeoTest.Classes
{
    abstract class GeoAbstractSrevice
    {
        protected string NameService;

        protected string TypeAnswer;

        protected string BaseUrl;

        protected string Address { get; set; }

        protected string FileName { get; set; }

        public GeoAbstractSrevice(string nameService, string typeAnswer, string baseUrl, string address, string fileName) 
        {
            this.NameService = nameService;
            this.TypeAnswer = typeAnswer;
            this.BaseUrl = baseUrl;
            this.Address = address;
            this.FileName = fileName;
        }

        public GeoAbstractSrevice() { }

        public abstract void DoRequest();

        protected abstract bool CheckParams();
    }
}
