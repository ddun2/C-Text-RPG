using System.Text.RegularExpressions;
using static SpartaDungeon.Program;

namespace SpartaDungeon
{
    // 아이템 정보를 저장하는 클래스
    public class Item
    {
        public string name;
        public string info;
        public int id;
        public int stat;
        public int type; // 무기 : 0, 방어구 : 1
        public int price;
        public bool isEquipped;
        public bool isSold;

        public Item(int id, string name, string info, int stat, int type, int price, bool isEquipped, bool isSold)
        {
            this.id = id;
            this.name = name;
            this.info = info;
            this.stat = stat;
            this.type = type;
            this.price = price;
            this.isEquipped = isEquipped;
            this.isSold = isSold;
        }        
    }
    // 플레이어 정보를 저장하는 클래스
    public class Player
    {
        public string name;
        public string job;
        public int level;
        public int baseAtk;
        public int baseDef;
        public int hp;
        public int gold;
        public int equippmentAtk;
        public int equippmentDef;
        public int equippedWeaponId;
        public int equippedArmorId;
        public int dungeonClearCount;
        public bool isEquippedWeapon;
        public bool isEquippedArmor;

        public Player()
        {
            level = 1;
            baseAtk = 10;
            baseDef = 5;
            hp = 100;
            gold = 1500;
            name = "Tav";
            job = "전사";
            dungeonClearCount = 0;
            isEquippedArmor = false;
            isEquippedWeapon = false;            
        }
    }
    internal class Program
    {
        static public Player player;
        static List<Item> shopItemData = new List<Item>();
        static List<Item> playerInventory = new List<Item>();  
        
        static void Main(string[] args)
        {
            player = new Player();
            InitShopData();            
            
            Lobby();
        }
                
        // 상점 아이템 초기화
        static void InitShopData()
        {
            shopItemData.Add(new Item(0, "낡은 갑옷", "수련에 도움을 주는 갑옷", 5, 1, 1000, false, false));
            shopItemData.Add(new Item(1, "무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷", 9, 1, 2200, false, false));
            shopItemData.Add(new Item(2, "가시 갑옷", "가시로 뒤덮힌 위협적인 갑옷", 15, 1, 3500, false, false));
            shopItemData.Add(new Item(3, "낡은 검", "쉽게 볼 수 있는 낡은 검", 2, 0, 600, false, false));
            shopItemData.Add(new Item(4, "청동 도끼", "어디선가 사용됐던 같은 도끼", 5, 0, 1500, false, false));
            shopItemData.Add(new Item(5, "팔카타", "스파르타 전사들의 도검", 7, 0, 3333, false, false));
        }
        // 아이템 구매시 인벤토리에 초기화
        static void InitInventoryData(int id)
        {
            playerInventory.Add(new Item(shopItemData[id].id, shopItemData[id].name, shopItemData[id].info, shopItemData[id].stat, shopItemData[id].type, shopItemData[id].price, shopItemData[id].isEquipped, shopItemData[id].isSold));
        }

        // 플레이어 상태를 출력하는 함수
        static public void PrintInfo()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($" Lv.{player.level}");
            Console.WriteLine($" {player.name} ( {player.job} )");
            Console.WriteLine($" 공격력\t  :\t{player.baseAtk + player.equippmentAtk} (+{player.equippmentAtk})");
            Console.WriteLine($" 방어력\t  :\t{player.baseDef + player.equippmentDef} (+{player.equippmentDef})");
            Console.WriteLine($" 체 력\t  :\t{player.hp}");
            Console.WriteLine($" Gold\t  :\t{player.gold} G");
            Console.WriteLine();
            Console.WriteLine(" 0. 나가기\n");
            Console.WriteLine(" 원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);

                if (act.Key == ConsoleKey.D0 || act.Key == ConsoleKey.NumPad0)
                {
                    Lobby();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }
            }
        }
        // 메인 화면
        static public void Lobby()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine(" 이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine(" 1. 상태 보기");
            Console.WriteLine(" 2. 인벤토리");
            Console.WriteLine(" 3. 상점");
            Console.WriteLine(" 4. 던전 입장");
            Console.WriteLine(" 5. 휴식하기\n");
            Console.WriteLine(" 원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);

                if (act.Key == ConsoleKey.D1 || act.Key == ConsoleKey.NumPad1)
                {
                    PrintInfo();
                    break;
                }
                else if (act.Key == ConsoleKey.D2 || act.Key == ConsoleKey.NumPad2)
                {
                    Inventory();
                    break;
                }
                else if (act.Key == ConsoleKey.D3 || act.Key == ConsoleKey.NumPad3)
                {
                    ShopManager();
                    break;
                }
                else if (act.Key == ConsoleKey.D4 || act.Key == ConsoleKey.NumPad4)
                {
                    Dungeon();
                    break;
                }
                else if (act.Key == ConsoleKey.D5 || act.Key == ConsoleKey.NumPad5)
                {
                    Rest();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }
            }
        }
        static public void Inventory()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 인벤토리\n 보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine(" [아이템 목록]");

            for (int i = 0; i < playerInventory.Count; i++)
            {
                if (playerInventory[i].type == 0)
                {
                    if (playerInventory[i].isEquipped)
                    {
                        Console.WriteLine($" - [E]{playerInventory[i].name}\t| 공격력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                    else
                    {
                        Console.WriteLine($" - {playerInventory[i].name}\t| 공격력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                }
                else
                {
                    if (playerInventory[i].isEquipped)
                    {
                        Console.WriteLine($" - [E]{playerInventory[i].name}\t| 방어력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                    else
                    {
                        Console.WriteLine($" - {playerInventory[i].name}\t| 방어력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine(" 1. 장착 관리\n" +
                " 0. 나가기\n");

            Console.WriteLine(" 원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);

                if (act.Key == ConsoleKey.D0 || act.Key == ConsoleKey.NumPad0)
                {
                    Lobby();
                    break;
                }
                else if (act.Key == ConsoleKey.D1 || act.Key == ConsoleKey.NumPad1)
                {
                    Equipment();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }
            }
        }
        // 장착 관리
        static public void Equipment()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 인벤토리 - 장착 관리\n 보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine(" [아이템 목록]");

            for (int i = 0; i < playerInventory.Count; i++)
            {
                if (playerInventory[i].type == 0)
                {
                    if (playerInventory[i].isEquipped)
                    {
                        Console.WriteLine($" - {i + 1}. [E]{playerInventory[i].name}\t| 공격력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                    else
                    {
                        Console.WriteLine($" - {i + 1}. {playerInventory[i].name}\t| 공격력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                }
                else
                {
                    if (playerInventory[i].isEquipped)
                    {
                        Console.WriteLine($" - {i + 1}. [E]{playerInventory[i].name}\t| 방어력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                    else
                    {
                        Console.WriteLine($" - {i + 1}. {playerInventory[i].name}\t| 방어력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine(" 0. 나가기\n");

            Console.WriteLine(" 원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);
               
                int inputKeyData = (int)act.Key;
                if (49 <= inputKeyData && inputKeyData <= 57)
                {
                    inputKeyData -= 48;
                }
                else if (97 <= inputKeyData && inputKeyData <= 105)
                {
                    inputKeyData -= 96;
                }


                if (act.Key == ConsoleKey.D0 || act.Key == ConsoleKey.NumPad0)
                {
                    Inventory();
                    break;
                }               
                else if (1 <= inputKeyData && inputKeyData <= playerInventory.Count)
                {
                    // 이미 장착 중인 아이템 선택
                    if (playerInventory[inputKeyData - 1].isEquipped)
                    {
                        Console.WriteLine(" 이미 장착 중인 아이템입니다.");
                        Thread.Sleep(1000);
                        Equipment();
                        break;
                    }
                    // 장착 중이지 않은 아이템 선택
                    else
                    {
                        // 아이템의 종류 판별
                            // 무기일때
                        if (playerInventory[inputKeyData - 1].type == 0)
                        {
                            // 이미 무기를 장착 중인지 확인
                            if(player.isEquippedWeapon)
                            {
                                // 인벤토리 무기 장착
                                playerInventory[inputKeyData - 1].isEquipped = true;
                                // 기존 장착 무기 해제
                                // 상점과 인벤토리의 순서가 다르기 때문에 아이템의 id 값으로 탐색하여 장착 해제
                                for (int i = 0; i < playerInventory.Count; i++)
                                {
                                    if (playerInventory[i].id == player.equippedWeaponId)
                                        playerInventory[i].isEquipped = false;
                                }
                                player.equippedWeaponId = playerInventory[inputKeyData - 1].id;
                                player.equippmentAtk = playerInventory[inputKeyData - 1].stat;
                                Equipment();
                                break;
                            }
                            else
                            {
                                playerInventory[inputKeyData - 1].isEquipped = true;
                                player.isEquippedWeapon = true;
                                player.equippedWeaponId = playerInventory[inputKeyData - 1].id;
                                player.equippmentAtk = playerInventory[inputKeyData - 1].stat;
                                Equipment();
                                break;
                            }
                        }
                            // 방어구일때
                        else if (playerInventory[inputKeyData - 1].type == 1)
                        {
                            // 이미 방어구를 장착 중인지 확인
                            if (player.isEquippedArmor)
                            {
                                playerInventory[inputKeyData - 1].isEquipped = true;
                                for (int i = 0; i < playerInventory.Count; i++)
                                {
                                    if (playerInventory[i].id == player.equippedArmorId)
                                        playerInventory[i].isEquipped = false;
                                }
                                //playerInventory[player.equippedArmorId].isEquipped = false;
                                player.equippedArmorId = playerInventory[inputKeyData - 1].id;
                                player.equippmentDef = playerInventory[inputKeyData - 1].stat;
                                Equipment();
                                break;
                            }
                            else
                            {
                                playerInventory[inputKeyData - 1].isEquipped = true;
                                player.isEquippedArmor = true;
                                player.equippedArmorId = playerInventory[inputKeyData - 1].id;
                                player.equippmentDef = playerInventory[inputKeyData - 1].stat;
                                Equipment();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                    continue;
                }
            }
        }
        // 상점
        static public void ShopManager()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 상점\n 필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($" [보유 골드]\n {player.gold} G\n");

            Console.WriteLine(" [아이템 목록]");

            for (int i = 0; i < shopItemData.Count; i++)
            {
                if (shopItemData[i].type == 0)
                {
                    if (shopItemData[i].isSold)
                    {
                        Console.WriteLine($" - {shopItemData[i].name}\t| 공격력 +{shopItemData[i].stat}\t| {shopItemData[i].info}\t\t| 구매완료");
                    }
                    else
                    {
                        Console.WriteLine($" - {shopItemData[i].name}\t| 공격력 +{shopItemData[i].stat}\t| {shopItemData[i].info}\t\t| {shopItemData[i].price} G");
                    }                    
                }
                else
                {
                    if (shopItemData[i].isSold)
                    {
                        Console.WriteLine($" - {shopItemData[i].name}\t| 방어력 +{shopItemData[i].stat}\t| {shopItemData[i].info}\t\t| 구매완료");
                    }
                    else
                    {
                        Console.WriteLine($" - {shopItemData[i].name}\t| 방어력 +{shopItemData[i].stat}\t| {shopItemData[i].info}\t\t| {shopItemData[i].price} G");
                    }
                }
            }

            Console.WriteLine();            
            Console.WriteLine(" 1. 아이템 구매");
            Console.WriteLine(" 2. 아이템 판매");
            Console.WriteLine(" 0. 나가기\n");

            Console.WriteLine(" 원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);

                if (act.Key == ConsoleKey.D0 || act.Key == ConsoleKey.NumPad0)
                {
                    Lobby();
                    break;
                }
                else if (act.Key == ConsoleKey.D1 || act.Key == ConsoleKey.NumPad1)
                {
                    BuyItem();
                    break;
                }
                else if (act.Key == ConsoleKey.D2 || act.Key == ConsoleKey.NumPad2)
                {
                    SellItem();
                    break;
                }
                else
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                    continue;
                }
            }
        }
        // 아이템 구매
        static public void BuyItem()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 상점\n 필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($" [보유 골드]\n {player.gold} G\n");

            Console.WriteLine(" [아이템 목록]");

            for (int i = 0; i < shopItemData.Count; i++)
            {
                if (shopItemData[i].type == 0)
                {
                    if (shopItemData[i].isSold)
                    {
                        Console.WriteLine($" - {shopItemData[i].id + 1}. {shopItemData[i].name}\t | 공격력 +{shopItemData[i].stat}\t| {shopItemData[i].info}\t\t| 구매완료");
                    }
                    else
                    {
                        Console.WriteLine($" - {shopItemData[i].id + 1}. {shopItemData[i].name}\t | 공격력 +{shopItemData[i].stat}\t| {shopItemData[i].info}\t\t| {shopItemData[i].price} G");
                    }
                }
                else
                {
                    if (shopItemData[i].isSold)
                    {
                        Console.WriteLine($" - {shopItemData[i].id + 1}. {shopItemData[i].name}\t | 방어력 +{shopItemData[i].stat}\t| {shopItemData[i].info}\t\t| 구매완료");
                    }
                    else
                    {
                        Console.WriteLine($" - {shopItemData[i].id + 1}. {shopItemData[i].name}\t | 방어력 +{shopItemData[i].stat}\t| {shopItemData[i].info}\t\t| {shopItemData[i].price} G");
                    }
                }
            }

            Console.WriteLine();            
            Console.WriteLine(" 0. 나가기\n");

            Console.WriteLine(" 원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);

                // 입력 받은 키값이 열거형으로 저장되어 있는 것을 확인함
                // 숫자키와 숫자패드의 지정된 상수값을 확인하고 정수로 변환하여 저장
                // 48~57, 96~105
                int inputKeyData = (int)act.Key;
                if (49 <= inputKeyData && inputKeyData <= 57)
                {
                    inputKeyData -= 48;
                }
                else if (97 <= inputKeyData && inputKeyData <= 105)
                {
                    inputKeyData -= 96;
                }

                if (act.Key == ConsoleKey.D0 || act.Key == ConsoleKey.NumPad0)
                {
                    ShopManager();
                    break;
                }
                // 입력 받은 값이 1~9 사이의 정수이면 숫자에 매칭되는 id값을 가진 아이템에 접근
                    // 1 ~ 상점 아이템 리스트의 크기 사이의 값을 받음
                else if (1 <= inputKeyData && inputKeyData <= shopItemData.Count)
                {
                    if (shopItemData[inputKeyData - 1].isSold)
                    {
                        Console.WriteLine(" 이미 구매한 아이템입니다.");
                        Thread.Sleep(1000); // 중복 구매, 골드 부족 등의 문구를 보여주기 위해 딜레이를 걸어줌
                        BuyItem();
                        break;
                    }
                    else
                    {
                        if(player.gold >= shopItemData[inputKeyData - 1].price)
                        {
                            shopItemData[inputKeyData - 1].isSold = true;                            
                            player.gold -= shopItemData[inputKeyData - 1].price;

                            InitInventoryData(inputKeyData - 1); // 인벤토리에 추가

                            BuyItem();
                            break;
                        }
                        else
                        {
                            Console.WriteLine(" 보유 Gold가 부족합니다.");
                            Thread.Sleep(1000);
                            continue;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                    continue;
                }
            }
        }
        // 아이템 판매
        static public void SellItem()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 상점 - 아이템 판매\n 필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($" [보유 골드]\n {player.gold} G\n");

            Console.WriteLine(" [아이템 목록]");

            for (int i = 0; i < playerInventory.Count; i++)
            {
                if (playerInventory[i].type == 0)
                {
                    if (playerInventory[i].isEquipped)
                    {
                        Console.WriteLine($" - {i + 1}. [E]{playerInventory[i].name}\t| 공격력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                    else
                    {
                        Console.WriteLine($" - {i + 1}. {playerInventory[i].name}\t| 공격력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                }
                else
                {
                    if (playerInventory[i].isEquipped)
                    {
                        Console.WriteLine($" - {i + 1}. [E]{playerInventory[i].name}\t| 방어력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                    else
                    {
                        Console.WriteLine($" - {i + 1}. {playerInventory[i].name}\t| 방어력 +{playerInventory[i].stat}\t| {playerInventory[i].info}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine(" 0. 나가기\n");

            Console.WriteLine(" 원하시는 행동을 입력해주세요.");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);

                int inputKeyData = (int)act.Key;
                if (49 <= inputKeyData && inputKeyData <= 57)
                {
                    inputKeyData -= 48;
                }
                else if (97 <= inputKeyData && inputKeyData <= 105)
                {
                    inputKeyData -= 96;
                }

                if (act.Key == ConsoleKey.D0 || act.Key == ConsoleKey.NumPad0)
                {
                    ShopManager();
                    break;
                }              
                else if (1 <= inputKeyData && inputKeyData <= playerInventory.Count)
                {
                    // 무기 & 방어구 판별
                    if (playerInventory[inputKeyData - 1].type == 0)
                    {
                        // 장착 해제
                        player.isEquippedWeapon = false;
                        player.equippedWeaponId = 0;
                        player.equippmentAtk = 0;
                        
                        for (int i = 0; i < shopItemData.Count; i++)
                        {
                            if (shopItemData[i].id == playerInventory[inputKeyData - 1].id)
                            {
                                shopItemData[i].isSold = false; // 상점에 물건 복귀
                            }                            
                        }

                        playerInventory.RemoveAt(inputKeyData - 1);
                        player.gold += shopItemData[inputKeyData - 1].price * 85 / 100; // 판매 골드 추가
                    }
                    else
                    {
                        player.isEquippedArmor = false;
                        player.equippedArmorId = 0;
                        player.equippmentDef = 0;
                        
                        for (int i = 0; i < shopItemData.Count; i++)
                        {
                            if (shopItemData[i].id == playerInventory[inputKeyData - 1].id)
                            {
                                shopItemData[i].isSold = false; // 상점에 물건 복귀
                            }
                        }

                        playerInventory.RemoveAt(inputKeyData - 1);
                        player.gold += shopItemData[inputKeyData - 1].price * 85 / 100;
                    }
                    SellItem();
                    break;
                }
                else
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                    continue;
                }
            }
        }
        // 던전 입장 화면
        static public void Dungeon()
        {
            //던전 클리어 후 입장 화면으로 돌아오면 레벨업 여부를 확인한다
            if (player.dungeonClearCount == player.level)
            {
                LevelUp();
            }

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 던전 입장");
            Console.WriteLine(" 이곳에서 던전으로 들어가기 전의 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine(" 1. 쉬운 던전 | 방어력 5 이상 권장");
            Console.WriteLine(" 2. 일반 던전 | 방어력 11 이상 권장");
            Console.WriteLine(" 3. 어려운 던전 | 방어력 17 이상 권장");
            Console.WriteLine(" 0. 나가기");
            Console.WriteLine();


            Console.WriteLine(" 원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);

                if (act.Key == ConsoleKey.D1 || act.Key == ConsoleKey.NumPad1)
                {
                    ClearDungeon(1);
                    break;
                }
                else if (act.Key == ConsoleKey.D2 || act.Key == ConsoleKey.NumPad2)
                {
                    ClearDungeon(2);
                    break;
                }
                else if (act.Key == ConsoleKey.D3 || act.Key == ConsoleKey.NumPad3)
                {
                    ClearDungeon(3);
                    break;
                }
                else if (act.Key == ConsoleKey.D0 || act.Key == ConsoleKey.NumPad0)
                {
                    Lobby();
                    break;
                }
                else
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                    continue;
                }
            }
        }
        // 던전 진행 & 클리어
        static public void ClearDungeon(int difficulty)
        {
            int playerDef = player.baseDef + player.equippmentDef;
            int playerAtk = player.baseAtk + player.equippmentAtk;
            int tmpHp = player.hp;
            int tmpGold = player.gold;
            int recommended_Def;
            int reward;
            Random random = new Random();

            switch (difficulty)
            {
                case 1:                    
                    reward = 1000;
                    recommended_Def = 5;
                    //입장에 필요한 최소 체력 판별
                    if (player.hp <= recommended_Def - playerDef + 35)
                    {
                        Console.WriteLine(" 입장에 필요한 체력이 부족합니다.");
                        Thread.Sleep(1000);
                        Dungeon();
                        break;
                    }
                    if (playerDef >= recommended_Def)
                    {
                        int hpDecrease = random.Next(20, 36) + (recommended_Def - playerDef);
                        tmpHp = player.hp - hpDecrease;
                        reward += reward * random.Next(playerAtk, playerAtk * 2) / 100;
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine(" 던전 클리어");
                        Console.WriteLine(" 축하합니다!!");
                        Console.WriteLine(" 쉬움 던전을 클리어 하였습니다.");
                        Console.WriteLine();
                        Console.WriteLine(" [탐험 결과]");
                        Console.WriteLine($" 체력 {player.hp} -> {tmpHp}");
                        Console.WriteLine($" Gold {tmpGold} G -> {reward + player.gold} G");
                        Console.WriteLine();

                        player.hp = tmpHp;
                        player.gold += reward;
                        player.dungeonClearCount++;
                        break;
                    }
                    else
                    {
                        int clearPercentage = random.Next(1, 11);
                        if (clearPercentage <= 4)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" 던전 실패");
                            Console.WriteLine(" 쉬움 던전에서 실패 하였습니다.");
                            Console.WriteLine();
                            Console.WriteLine(" [탐험 결과]");
                            Console.WriteLine($" 체력 {player.hp} -> {player.hp / 2}");
                            Console.WriteLine($" Gold {player.gold} G -> {player.gold} G");
                            Console.WriteLine();

                            player.hp = player.hp / 2;
                            break;
                        }
                        else
                        {
                            int hpDecrease = random.Next(20, 36) + (recommended_Def - playerDef);
                            tmpHp = player.hp - hpDecrease;
                            reward += reward * random.Next(playerAtk, playerAtk * 2) / 100;
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" 던전 클리어");
                            Console.WriteLine(" 축하합니다!!");
                            Console.WriteLine(" 쉬움 던전을 클리어 하였습니다.");
                            Console.WriteLine();
                            Console.WriteLine(" [탐험 결과]");
                            Console.WriteLine($" 체력 {player.hp} -> {tmpHp}");
                            Console.WriteLine($" Gold {tmpGold} G -> {reward + player.gold} G");
                            Console.WriteLine();

                            player.hp = tmpHp;
                            player.gold += reward;
                            break;
                        }
                    }

                case 2:                    
                    reward = 1700;
                    recommended_Def = 11;

                    if (player.hp <= recommended_Def - playerDef + 20)
                    {
                        Console.WriteLine(" 입장에 필요한 체력이 부족합니다.");
                        Thread.Sleep(1000);
                        Dungeon();
                        break;
                    }

                    if (playerDef >= recommended_Def)
                    {
                        int hpDecrease = random.Next(20, 36) + (recommended_Def - playerDef);
                        tmpHp = player.hp - hpDecrease;
                        reward += reward * random.Next(playerAtk, playerAtk * 2) / 100;
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine(" 던전 클리어");
                        Console.WriteLine(" 축하합니다!!");
                        Console.WriteLine(" 일반 던전을 클리어 하였습니다.");
                        Console.WriteLine();
                        Console.WriteLine(" [탐험 결과]");
                        Console.WriteLine($" 체력 {player.hp} -> {tmpHp}");
                        Console.WriteLine($" Gold {tmpGold} G -> {reward + player.gold} G");
                        Console.WriteLine();

                        player.hp = tmpHp;
                        player.gold += reward;
                        break;
                    }
                    else
                    {
                        int clearPercentage = random.Next(1, 11);
                        if (clearPercentage <= 4)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" 던전 실패");
                            Console.WriteLine(" 일반 던전에서 실패 하였습니다.");
                            Console.WriteLine();
                            Console.WriteLine(" [탐험 결과]");
                            Console.WriteLine($" 체력 {player.hp} -> {player.hp / 2}");
                            Console.WriteLine($" Gold {player.gold} G -> {player.gold} G");
                            Console.WriteLine();

                            player.hp = player.hp / 2;
                            break;
                        }
                        else
                        {
                            int hpDecrease = random.Next(20, 36) + (recommended_Def - playerDef);
                            tmpHp = player.hp - hpDecrease;
                            reward += reward * random.Next(playerAtk, playerAtk * 2) / 100;
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" 던전 클리어");
                            Console.WriteLine(" 축하합니다!!");
                            Console.WriteLine(" 일반 던전을 클리어 하였습니다.");
                            Console.WriteLine();
                            Console.WriteLine(" [탐험 결과]");
                            Console.WriteLine($" 체력 {player.hp} -> {tmpHp}");
                            Console.WriteLine($" Gold {tmpGold} G -> {reward + player.gold} G");
                            Console.WriteLine();

                            player.hp = tmpHp;
                            player.gold += reward;
                            break;
                        }
                    }                    
                case 3:
                    reward = 2500;
                    recommended_Def = 17;

                    if (player.hp <= recommended_Def - playerDef + 20)
                    {
                        Console.WriteLine(" 입장에 필요한 체력이 부족합니다.");
                        Thread.Sleep(1000);
                        Dungeon();
                        break;
                    }

                    if (playerDef >= recommended_Def)
                    {
                        int hpDecrease = random.Next(20, 36) + (recommended_Def - playerDef);
                        tmpHp = player.hp - hpDecrease;
                        reward += reward * random.Next(playerAtk, playerAtk * 2) / 100;
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine(" 던전 클리어");
                        Console.WriteLine(" 축하합니다!!");
                        Console.WriteLine(" 어려움 던전을 클리어 하였습니다.");
                        Console.WriteLine();
                        Console.WriteLine(" [탐험 결과]");
                        Console.WriteLine($" 체력 {player.hp} -> {tmpHp}");
                        Console.WriteLine($" Gold {tmpGold} G -> {reward + player.gold} G");
                        Console.WriteLine();

                        player.hp = tmpHp;
                        player.gold += reward;
                        break;
                    }
                    else
                    {
                        int clearPercentage = random.Next(1, 11);
                        if (clearPercentage <= 4)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" 던전 실패");
                            Console.WriteLine(" 어려움 던전에서 실패 하였습니다.");
                            Console.WriteLine();
                            Console.WriteLine(" [탐험 결과]");
                            Console.WriteLine($" 체력 {player.hp} -> {player.hp / 2}");
                            Console.WriteLine($" Gold {player.gold} G -> {player.gold} G");
                            Console.WriteLine();

                            player.hp = player.hp / 2;
                            break;
                        }
                        else
                        {
                            int hpDecrease = random.Next(20, 36) + (recommended_Def - playerDef);
                            tmpHp = player.hp - hpDecrease;
                            reward += reward * random.Next(playerAtk, playerAtk * 2) / 100;
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" 던전 클리어");
                            Console.WriteLine(" 축하합니다!!");
                            Console.WriteLine(" 어려움 던전을 클리어 하였습니다.");
                            Console.WriteLine();
                            Console.WriteLine(" [탐험 결과]");
                            Console.WriteLine($" 체력 {player.hp} -> {tmpHp}");
                            Console.WriteLine($" Gold {tmpGold} G -> {reward + player.gold} G");
                            Console.WriteLine();

                            player.hp = tmpHp;
                            player.gold += reward;
                            break;
                        }
                    }
                }
            Console.WriteLine(" 0. 나가기\n");
            Console.WriteLine(" 원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);

                if (act.Key == ConsoleKey.D0 || act.Key == ConsoleKey.NumPad0)
                {
                    Dungeon();
                    break;
                }                
                else
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                    continue;
                }
            }
        }
        // 휴식
        static public void Rest()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 휴식하기");
            Console.WriteLine($" 500 G를 내면 체력을 회복 할 수 있습니다. (보유 골드 : {player.gold} G)");
            Console.WriteLine();
            Console.WriteLine(" 1. 휴식하기");
            Console.WriteLine(" 0. 나가기");
            Console.WriteLine();

            Console.WriteLine(" 원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);

                if (act.Key == ConsoleKey.D1 || act.Key == ConsoleKey.NumPad1)
                {
                    if (player.gold >= 500)
                    {
                        Console.WriteLine(" 휴식을 완료했습니다.");
                        player.hp = 100;
                        player.gold -= 500;
                        Thread.Sleep(1000);
                        Rest();
                        break;
                    }
                    else
                    {
                        Console.WriteLine(" Gold가 부족합니다.");
                        Thread.Sleep(1000);
                        Rest();
                        break;
                    }
                }                
                else if (act.Key == ConsoleKey.D0 || act.Key == ConsoleKey.NumPad0)
                {
                    Lobby();
                    break;
                }
                else
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                    continue;
                }
            }
        }
        // 레벨업 기능
        static public void LevelUp()
        {
            player.level++;
            player.dungeonClearCount = 0;
            player.baseAtk ++;
            player.baseDef ++;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 레벨 업!");
            Console.WriteLine(" 축하합니다.");
            Console.WriteLine($" {player.level} 레벨로 상승했습니다.");

            //Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine(" 0. 나가기");
            Console.WriteLine();

            Console.WriteLine(" 원하시는 행동을 입력해주세요.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> ");
                Console.ResetColor();
                ConsoleKeyInfo act = Console.ReadKey(true);

                if (act.Key == ConsoleKey.D0 || act.Key == ConsoleKey.NumPad0)
                {
                    
                    break;
                }
                else
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                    continue;
                }
            }

        }
    }
}
