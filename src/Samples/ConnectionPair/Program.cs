﻿using System;
using CRA.ClientLibrary;

namespace ConnectionPair
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new CRAClientLibrary();

            client.DefineProcess("connectionpairprocess", () => new ConnectionPairProcess());

            client.InstantiateProcess("crainst01", "process1", "connectionpairprocess", null);
            client.InstantiateProcess("crainst02", "process2", "connectionpairprocess", null);

            client.Connect("process1", "output", "process2", "input");
            client.Connect("process2", "output", "process1", "input");

            Console.ReadLine();
        }
    }
}
