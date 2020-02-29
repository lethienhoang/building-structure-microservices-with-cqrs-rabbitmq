using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Framework.Mongo
{
    public class MongoDbOptions
    {
        public string ConnectionString { get; set; }

        public string Database { get; set; }

        public bool Seed { get; set; }
    }
}
