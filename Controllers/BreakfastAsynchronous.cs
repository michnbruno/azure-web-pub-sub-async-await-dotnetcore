using Azure.Messaging.WebPubSub;

namespace PubSub.Test1.Controllers
{
    static class BreakfastAsynchronous
    {

        public static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        public static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        public static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        public static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        public static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        public static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        public static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        public static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }

        public static async Task<bool> MakeAsyncBreakfastAsync(WebPubSubServiceClient pubSubClient)
        {
            pubSubClient.SendToAll("Starting Async Breakfast....");
            //return new string[] { "value1", "value2" };
            Coffee cup = BreakfastSynchronous.PourCoffee();
            Console.WriteLine("coffee is ready");
            pubSubClient.SendToAll("coffee is ready");


            pubSubClient.SendToAll("starting eggs, bacon and toast asynchronously....");
            var eggsTask = BreakfastAsynchronous.FryEggsAsync(2);
            var baconTask = BreakfastAsynchronous.FryBaconAsync(3);
            var toastTask = BreakfastAsynchronous.MakeToastWithButterAndJamAsync(2);
            var cntStart = 3;
            var cntCur = 0;
            var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                cntCur = breakfastTasks.Count;
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                    pubSubClient.SendToAll("eggs are ready");
                }
                else if (finishedTask == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                    pubSubClient.SendToAll("bacon is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Console.WriteLine("toast is ready");
                    pubSubClient.SendToAll("toast is ready");
                }
                await finishedTask;
                breakfastTasks.Remove(finishedTask);

            }

            Juice oj = BreakfastSynchronous.PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
            pubSubClient.SendToAll("oj is ready");
            pubSubClient.SendToAll("<strong>Breakfast is ready!</strong>");

            return true;

        }

    }
}
