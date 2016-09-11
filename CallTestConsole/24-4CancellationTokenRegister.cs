using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace CallTestConsole
{
    public class _24_4CancellationTokenRegister
    {
        public void Dowork()
        {
            CancellationTokenSource cts1 = new CancellationTokenSource();
            CancellationTokenSource cts2 = new CancellationTokenSource();
            cts1.Token.Register(() =>
            {
                Console.WriteLine("cts1 was canceled");
            });
            cts2.Token.Register(() =>
            {
                Console.WriteLine("cts2 was canceled");
            });
            CancellationTokenSource ctslinked = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);
            ctslinked.Token.Register(() =>
            {
                Console.WriteLine("cts linked was canceled");

            });
            cts2.Cancel();
            cts1.Cancel();
            Console.ReadKey();
        }
    }
}
