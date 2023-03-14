using Azure.Messaging.WebPubSub;

namespace PubSub.Test1.Controllers
{

    static class BreakfastSynchronous
    {
        public static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        public static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        public static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        public static Toast ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        public static Bacon FryBacon(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            Task.Delay(3000).Wait();
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        public static Egg FryEggs(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            Task.Delay(3000).Wait();
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        public static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }

        public static bool MakeSyncBreakfast(WebPubSubServiceClient pubSubClient)
        {
            pubSubClient.SendToAll("Starting Sync Breakfast....");
            //return new string[] { "value1", "value2" };
            Coffee cup = BreakfastSynchronous.PourCoffee();
            Console.WriteLine("coffee is ready");
            pubSubClient.SendToAll("coffee is ready");

            pubSubClient.SendToAll("starting eggs....");
            Egg eggs = BreakfastSynchronous.FryEggs(2);
            Console.WriteLine("eggs are ready");
            pubSubClient.SendToAll("eggs are ready");

            pubSubClient.SendToAll("starting bacon....");
            Bacon bacon = BreakfastSynchronous.FryBacon(3);
            Console.WriteLine("bacon is ready");
            pubSubClient.SendToAll("bacon is ready");

            pubSubClient.SendToAll("starting toast....");
            Toast toast = BreakfastSynchronous.ToastBread(2);
            BreakfastSynchronous.ApplyButter(toast);
            BreakfastSynchronous.ApplyJam(toast);
            Console.WriteLine("toast is ready");
            pubSubClient.SendToAll("toast is ready");

            Juice oj = BreakfastSynchronous.PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
            pubSubClient.SendToAll("oj is ready");
            pubSubClient.SendToAll("<strong>Breakfast is ready</strong>");

            return true;
        }
    }
}
