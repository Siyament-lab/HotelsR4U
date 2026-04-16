namespace HotelsR4U.Menus
{
    public abstract class MenuBase
    {
        public void ShowMenu ()
        {
            bool running = true;

            while (running)
            {
                //Huvudmeny för entitet hantering

                Console.Clear ();
                Console.WriteLine (GetMenuTitle ());
                Console.WriteLine ("1. Visa alla");
                Console.WriteLine ("2. Lägg till");
                Console.WriteLine ("3. Uppdatera");
                Console.WriteLine ("4. Ta bort");
                Console.WriteLine ("0. Tillbaka");
                Console.Write ("Välj: ");

                var choice = Console.ReadLine ();

                switch (choice)
                {
                    case "1":
                        ShowAll ();
                        break;
                    case "2":
                        Add ();
                        break;
                    case "3":
                        Update ();
                        break;
                    case "4":
                        Delete ();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine ("Ogiltigt val.");
                        Pause ();
                        break;
                }
            }
        }

        protected abstract string GetMenuTitle ();
        protected abstract void ShowAll ();
        protected abstract void Add ();
        protected abstract void Update ();
        protected abstract void Delete ();

        protected void Pause ()
        {
            Console.WriteLine ("\nTryck valfri tangent för att fortsätta...");
            Console.ReadKey ();
        }
    }
}