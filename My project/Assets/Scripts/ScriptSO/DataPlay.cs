
public enum NAME_MAP
{
    loading,
    menu,
    lv1,
    lv2, 
    lv3
}
public enum NAME_KEY_PREFABS
{
    hpPlayer,
    mpPlayer,
    numberCoin
}
public static class DataPlay
{
    static bool startGame = true;
    static int typeLanguage = 0;
    static float coin;
    static string[] NameMap = { "Loading", "Menu", "Lv1", "Lv2", "Lv3" };
    static string[] KeyPrefabs = { "HP_PLAYER", "MP_PLAYER", "NUMBER_COIN" };

    public static bool StartGame
    {
        get { return startGame; }
        set { startGame = value; }
    }
    public static int TypeLanguage
    {
        get { return typeLanguage; }
        set { typeLanguage = value; }
    }
    public static float Coin
    {
        get { return coin; }
        set { coin = value; }
    }
    public static string Name(NAME_MAP index)
    {
        return NameMap[(int)index];
    }
    public static string NameKey(NAME_KEY_PREFABS index)
    {
        return KeyPrefabs[(int)index];
    }
}
