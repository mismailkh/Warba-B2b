public class MenuDataService
{
    private List<MainMenuItems> MenuData = new List<MainMenuItems>()
    {

        new MainMenuItems (
            path: "/dashboard",
            type: "link",
            title: "Dashboard",
            icon: "bx bx-home",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems(
            menuTitle: "MAIN"
        ),
        new MainMenuItems(
            type: "sub",
            title: "Motor Comp",
            icon: "bi bi-car-front",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
              new MainMenuItems (
                    path: "/save-policy",
                    type: "link",
                    title: "New Policy",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/motor-tpl-schemes",
                    type: "link",
                    title: "Policies",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/list-quotations",
                    type: "link",
                    title: "Quotations",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
        new MainMenuItems(
            type: "sub",
            title: "Motor TPL",
            icon: "bi bi-ev-front",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
              new MainMenuItems (
                    path: "/save-policy",
                    type: "link",
                    title: "New Policy",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/motor-tpl-schemes",
                    type: "link",
                    title: "Policies",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/travel-packages",
                    type: "link",
                    title: "Quotations",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
        new MainMenuItems(
            type: "sub",
            title: "Marine TPL",
            icon: "bi bi-percent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
              new MainMenuItems (
                    path: "/save-policy",
                    type: "link",
                    title: "New Policy",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/motor-tpl-schemes",
                    type: "link",
                    title: "Policies",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/travel-packages",
                    type: "link",
                    title: "Quotations",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
        new MainMenuItems(
            type: "sub",
            title: "Travel",
            icon: "bi bi-airplane",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
              new MainMenuItems (
                    path: "/save-policy",
                    type: "link",
                    title: "New Policy",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/motor-tpl-schemes",
                    type: "link",
                    title: "Policies",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/travel-packages",
                    type: "link",
                    title: "Quotations",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
            }
        ),
        new MainMenuItems (
            menuTitle: "GENERAL"
        ),
        new MainMenuItems (
            path: "/reports",
            type: "link",
            title: "Reports",
            icon: "bi bi-graph-up-arrow",
            selected: false,
            active: false,
            dirChange: false
        )
    };

    public List<MainMenuItems> GetMenuData()
    {
        return MenuData;
    }
}
