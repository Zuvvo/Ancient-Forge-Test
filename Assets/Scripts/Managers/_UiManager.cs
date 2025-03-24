using System.Linq;
using UnityEngine;

public class _UiManager : Singleton<_UiManager>
{
    [SerializeField] private UiScreen[] _uiScreens;

    public UiScreen GetScreenRefOfType(EScreenType screenType)
    {
        return _uiScreens.FirstOrDefault(x => x.ScreenType == screenType);
    }
}
