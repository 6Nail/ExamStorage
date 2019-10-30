using DbUp;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Storage
{
    class Program
    {
        public const string CONNECTION_STRING = "Server=A-305-08;Database=BusDepos;Trusted_Connection=True;";
        public void PrintKeeper()
        {
            List<StoreKeeper> storeKeepers = new List<StoreKeeper>
            {

            };
        }
static void Main(string[] args)
        {
            
            EnsureDatabase.For.SqlDatabase(CONNECTION_STRING);

            var upgrader =
              DeployChanges.To
                  .SqlDatabase(CONNECTION_STRING)
                  .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                  .LogToConsole()
                  .Build();

            upgrader.PerformUpgrade();
            WayBillRepository wayBillRepository = new WayBillRepository(CONNECTION_STRING);
            StoreKeeperRepository storeKeeperRepository = new StoreKeeperRepository(CONNECTION_STRING);

            while (true)
            {
                Console.WriteLine("Выберите меню: \n" +
                    "1.Принять товар\n" +
                    "2.Отправить товар\n" +
                    "3.Изменить товар\n" +
                    "4.Список товаров\n" +
                    "5.Список кладовщиков\n"+
                    "0.Exit\n" );
                int chooseMenu;
                if (int.TryParse(Console.ReadLine(), out chooseMenu))
                {
                    Console.Clear();
                    switch (chooseMenu)
                    {
                        case 1:
                            insetWayBill(storeKeeperRepository);
                           
                            break;
                        case 2:
                            WayBillDepart(storeKeeperRepository);



                            break;
                        case 3:
                            UpdateWayBill(storeKeeperRepository);



                            break;
                        case 4:
                            ProductRepository productRepository = new ProductRepository(CONNECTION_STRING);
                            var products = productRepository.GetProduct() as List<Product>;
                            PrintProduct(products);
                            break;
                        case 5:
                            var keepers = storeKeeperRepository.GetStoreKeeper() as List<StoreKeeper>;
                            PrintKeeper(keepers);
                            int i = 0;
                           
                            break;
                    }

                }
                
            }
        }

        private static void UpdateWayBill(StoreKeeperRepository storeKeeperRepository)
        {
            WayBillRepository wayBillRepository = new WayBillRepository(CONNECTION_STRING);
            var updwaybil = wayBillRepository.GetWayBill() as List<Waybill>;
            int i = 0;
            Console.WriteLine("# | Id WayBill | Send | Provider | Id Keeper | Date To/from");
            foreach (var waybill in updwaybil)
            {
                if (waybill.IsExport==true)
                {

                    Console.WriteLine($"{++i},{waybill.Id},{waybill.IsExport},{waybill.StoreKeeperId},{waybill.DeliveryDate},");

                }
                else
                {
                    Console.WriteLine($"{++i},{waybill.Id},{waybill.IsExport},{waybill.StoreKeeperId},{waybill.DepartDate},");

                }
                while (true)
                {
                    int choise = 0;
                    if (int.TryParse(Console.ReadLine(), out choise))
                    {
                        choise--;
                        if (choise>=0&&choise<updwaybil.Count)
                        {
                            if (updwaybil[choise].IsExport == true)
                            {
                                Console.WriteLine("Изменить получателя");
                                updwaybil[choise].Receiver = Console.ReadLine();
                            }
                        }
                       
                    

                }
            }
        }

    private static void insetWayBill(StoreKeeperRepository storeKeeperRepository)
        {
            Waybill WayAdd = new Waybill();
            while (true)
            {

                Console.WriteLine("Выберете кладовщика");
                var keeperId = storeKeeperRepository.GetStoreKeeper() as List<StoreKeeper>;
                int chooseKeeper;
                if (int.TryParse(Console.ReadLine(), out chooseKeeper))
                {
                    --chooseKeeper;
                    if (chooseKeeper >= 0 && chooseKeeper < keeperId.Count)
                    {
                        WayAdd.StoreKeeperId = keeperId[chooseKeeper].Id;
                        break;
                    }
                }
            }
            WayAdd.IsExport = false;
             Console.WriteLine("Id Provider");
             WayAdd.Provider = Console.ReadLine();
            WayAdd.DeliveryDate = DateTime.Now;
             
        }
        private static void WayBillDepart(StoreKeeperRepository storeKeeperRepository)
        {
            Waybill Depart = new Waybill();
            while (true)
            {

                Console.WriteLine("Выберете кладовщика");
                var keeperId = storeKeeperRepository.GetStoreKeeper() as List<StoreKeeper>;
                int chooseKeeper;
                if (int.TryParse(Console.ReadLine(), out chooseKeeper))
                {
                    --chooseKeeper;
                    if (chooseKeeper >= 0 && chooseKeeper < keeperId.Count)
                    {
                        Depart.StoreKeeperId = keeperId[chooseKeeper].Id;
                        break;
                    }
                }
            }
            Depart.IsExport = false;
            Console.WriteLine("Id Provider");
            Depart.Provider = Console.ReadLine();
            Depart.DepartDate = DateTime.Now;
            
        }

        private static void PrintKeeper(List<StoreKeeper> keepers)
        {
            foreach (var keeper in keepers)
            {
                Console.WriteLine($"{++i},{keeper.Id},{ keeper.Name}");
            }
        }

        private static void PrintProduct(List<Product> products)
        {
            Console.WriteLine("Id product | name product | Quantity product | Id WayBill");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id},{product.Name},{product.Quantity},{product.Price}");
            }
        }
    }
}
