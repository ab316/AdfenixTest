using System;
using Starcounter;

namespace Adfenix
{
    class Program
    {
        static void Main()
        {
            Db.Transact(() =>
            {
                var anyone = Db.SQL<Corporation>("SELECT c from Corporation c").First;
                if (null == anyone)
                {
                    new Corporation
                    {
                        name = "Stockholm Star"
                    };
                }
            });
        }
    }
}