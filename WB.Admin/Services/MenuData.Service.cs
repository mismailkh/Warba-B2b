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
        new MainMenuItems (
            path: "/lobs",
            type: "link",
            title: "LOBs",
            icon: "bx bx-briefcase",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems(
            type: "sub",
            title: "Packages & Schemes",
            icon: "bi bi-percent",
            selected: false,
            active: false,
            dirChange: false,
            children: new MainMenuItems[]
            {
              new MainMenuItems (
                    path: "/motor-comp-packages",
                    type: "link",
                    title: "Motor Comp",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/motor-tpl-schemes",
                    type: "link",
                    title: "Motor TPL",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/travel-packages",
                    type: "link",
                    title: "Travel",
                    selected: false,
                    active: false,
                    dirChange: false
                ),
              new MainMenuItems (
                    path: "/marine-packages",
                    type: "link",
                    title: "Marine TPL",
                    selected: false,
                    active: false,
                    dirChange: false
                )
            }
        ),
        new MainMenuItems (
            path: "/addons",
            type: "link",
            title: "Add-Ons",
            icon: "bi bi-list-check",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems (
            path: "/discounts",
            type: "link",
            title: "Discounts",
            icon: "bx bx-gift",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems(
            menuTitle: "ORGANIZATION MGMT"
        ),
        new MainMenuItems (
            path: "/organization-types",
            type: "link",
            title: "Organization Types",
            icon: "bi bi-list-nested",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems (
            path: "/organizations",
            type: "link",
            title: "Organizations",
            icon: "bi bi-building",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems (
            path: "/designations",
            type: "link",
            title: "Designations",
            icon: "bi bi-person-vcard",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems(
            menuTitle: "Authorization"
        ),
        new MainMenuItems (
            path: "/authorization-levels",
            type: "link",
            title: "Authorization Levels",
            icon: "bx bx-fingerprint",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems (
            path: "/acces-categories",
            type: "link",
            title: "Access Categories",
            icon: "bi bi-toggles",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems (
            path: "/users",
            type: "link",
            title: "Users",
            icon: "bi bi-people",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems (
            menuTitle: "GENERAL"
        ),
        new MainMenuItems (
            path: "/lookups",
            type: "link",
            title: "Lookups",
            icon: "bx bx-slider",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems (
            path: "/audit-logs",
            type: "link",
            title: "Audit Logs",
            icon: "bi bi-journal-medical",
            selected: false,
            active: false,
            dirChange: false
        ),
        new MainMenuItems (
            path: "/documents",
            type: "link",
            title: "Documents",
            icon: "bx bx-paperclip",
            selected: false,
            active: false,
            dirChange: false
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
