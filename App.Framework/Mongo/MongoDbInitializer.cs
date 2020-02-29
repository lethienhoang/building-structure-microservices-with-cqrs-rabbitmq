﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Framework.Mongo
{
    public class MongoDbInitializer : IMongoDbInitializer
    {
        private static bool _initialized;
        private readonly bool _seed;
        private readonly IMongoDatabase _database;
        private readonly IMongoDbSeeder _seeder;

        public MongoDbInitializer(IMongoDatabase database,
           IMongoDbSeeder seeder,
           MongoDbOptions options)
        {
            _database = database;
            _seeder = seeder;
            _seed = options.Seed;
        }

        public async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;

            if (!_seed)
            {
                return;
            }

            await _seeder.SeedAsync();
        }
    }
}