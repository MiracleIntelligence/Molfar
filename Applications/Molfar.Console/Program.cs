using Molfar.CoinCap;
using Molfar.Console.Services;
using Molfar.Core.Services;
using Molfar.Models.Services;
using Molfar.NEM;
using Molfar.Notes;
using Molfar.Spoco;
using SimpleInjector;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Molfar.Console
{
    internal class Program
    {
        private static Core.Molfar _molfar;

        private static void Main(string[] args)
        {
            System.Console.OutputEncoding = Encoding.UTF8;
            //System.Console.Write("Input: ");
            //var st = ReadLine();
            //System.Console.WriteLine("Output: {0}", st);
            //System.Console.ReadKey();

            //System.Console.OutputEncoding = Encoding.UTF8;
            //System.Console.InputEncoding = Encoding.UTF8;

            //test();
            Container container = new Container();

            container.Register<ISettingsService, SettingsService>();
            container.Register<DatabaseService>();
            //container.Register<WeeterAppService>(Lifestyle.Singleton);

            _molfar = new Core.Molfar();
            _molfar.Answered += OnMolfarAnswered;

            _molfar.Initialize(container);
            _molfar.Install<MolfarNotesInstaller>();
            _molfar.Install<MolfarCoinCapInstaller>();
            _molfar.Install<NemInstaller>();
            _molfar.Install<MolfarSpocoInstaller>();
            //_molfar.Install<MolfarWeeterInstaller>();

            while (_molfar.Active)
            {
                _molfar.SendMessage(ReadLine());
            }
            //System.Console.WriteLine("Hello World!");
            //System.Console.ReadKey(true);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        static extern bool ReadConsoleW(IntPtr hConsoleInput, [Out] byte[]
           lpBuffer, uint nNumberOfCharsToRead, out uint lpNumberOfCharsRead,
           IntPtr lpReserved);

        public static IntPtr GetWin32InputHandle()
        {
            const int STD_INPUT_HANDLE = -10;
            IntPtr inHandle = GetStdHandle(STD_INPUT_HANDLE);
            return inHandle;
        }

        public static string ReadLine()
        {
            const int bufferSize = 1024;
            var buffer = new byte[bufferSize];

            uint charsRead = 0;

            ReadConsoleW(GetWin32InputHandle(), buffer, bufferSize, out charsRead, (IntPtr)0);
            // -2 to remove ending \n\r
            int nc = ((int)charsRead - 2) * 2;
            var b = new byte[nc];
            for (var i = 0; i < nc; i++)
                b[i] = buffer[i];

            var utf8enc = Encoding.UTF8;
            var unicodeenc = Encoding.Unicode;
            return utf8enc.GetString(Encoding.Convert(unicodeenc, utf8enc, b));
        }

        private static void test()
        {
            //System.Console.OutputEncoding = Encoding.UTF8;
            // Create a Char array for the modern Cyrillic alphabet, 
            // from U+0410 to U+044F.
            int nChars = 0x04FF - 0x0410 + 1;
            char[] chars = new char[nChars];
            ushort codePoint = 0x0410;
            for (int ctr = 0; ctr < chars.Length; ctr++)
            {
                chars[ctr] = Convert.ToChar(codePoint);
                codePoint++;
            }

            System.Console.WriteLine("Current code page: {0}\n",
                              System.Console.OutputEncoding.CodePage);
            // Display the characters.
            foreach (var ch in chars)
            {
                System.Console.Write("{0}  ", ch);
                if (System.Console.CursorLeft >= 70)
                    System.Console.WriteLine();
            }
        }

        private static void OnMolfarAnswered(object sender, Core.Models.IMolfarAnswer e)
        {
            foreach (var row in e.GetAnswer())
            {
                System.Console.WriteLine($"<< {row}");
            }
        }
    }
}
