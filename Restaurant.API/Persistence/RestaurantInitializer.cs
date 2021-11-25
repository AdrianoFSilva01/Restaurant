namespace Restaurant.API.Persistence;
using Restaurant.API.Entitites;
using Restaurant.API.Entitites.CatalogEntities;
using Restaurant.API.Entitites.CategoryEntities;
using Restaurant.API.Entitites.IngredientCategoryEntities;
using System.Collections.Generic;
using System.Linq;

public class RestaurantInitializer
{
    private Dictionary<int, Category> Categories;
    private Dictionary<int, Catalog> Catalogs;
    private Dictionary<int, CatalogInfo> CatalogInfos;
    private Dictionary<int, Ingredient> Ingredients;
    private Dictionary<int, IngredientCategory> IngredientCategories;
    private Dictionary<int, IngredientSubCategory> IngredientSubCategories;

    public static void Initialize(RestaurantDbContext context)
    {
        RestaurantInitializer initializer = new RestaurantInitializer();
        initializer.SeedEverything(context);
    }

    public void SeedEverything(RestaurantDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (context.Categories.Any())
        {
            return;
        }

        SeedIngredientCategories(context);

        SeedIngredientSubCategories(context);

        SeedIngredients(context);

        SeedCategories(context);

        SeedCatalog(context);

        SeedCatalogInfo(context);

        SeedCatalogIngredient(context);
    }

    public void SeedCategories(RestaurantDbContext context)
    {
        Categories = new Dictionary<int, Category>()
            {
                {1, new Category { Name = "Entradas", ImageUrl = "http://svgshare.com/i/DvT.svg" } },
                {2, new Category { Name = "Carnes", ImageUrl = "http://svgshare.com/i/Dwz.svg" } },
                {3, new Category { Name = "Peixes", ImageUrl = "http://svgshare.com/i/DxV.svg" } },
                {4, new Category { Name = "Bebidas", ImageUrl = "http://svgshare.com/i/DwK.svg" } },
                {5, new Category { Name = "Sobremesa", ImageUrl = "http://svgshare.com/i/Dx0.svg" } }
        };
        context.Categories.AddRange(Categories.Values);

        context.SaveChanges();
    }

    public void SeedIngredientSubCategories(RestaurantDbContext context)
    {
        int id = 1;
        Dictionary<int, IngredientSubCategory> common;
        IngredientSubCategories = new Dictionary<int, IngredientSubCategory>();

        common = new Dictionary<int, IngredientSubCategory>()
            {
                { id++, new IngredientSubCategory { Name = "Água", ImageUrl = "www.agua.com" } },
                { id++, new IngredientSubCategory { Name = "Álcoolicas", ImageUrl = "www.alcool.com" } },
                { id++, new IngredientSubCategory { Name = "Refrigerantes", ImageUrl = "www.refrigerantes.com" } },
                { id++, new IngredientSubCategory { Name = "Bebidas Quantes", ImageUrl = "www.bebidasquentes.com" } }
            };
        IngredientCategories[1].IngredientSubCategories = common.Values.ToList();
        IngredientSubCategories = IngredientSubCategories.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, IngredientSubCategory>()
            {
                { id++, new IngredientSubCategory { Name = "Enchidos Regionais", ImageUrl = "www.enchidos.com" } },
                { id++, new IngredientSubCategory { Name = "Fatiados", ImageUrl = "www.fatiados.com" } },
                { id++, new IngredientSubCategory { Name = "Fiambres", ImageUrl = "www.fiambres.com" } },
                { id++, new IngredientSubCategory { Name = "Queijos", ImageUrl = "www.queijos.com" } },
                { id++, new IngredientSubCategory { Name = "Patês", ImageUrl = "www.patês.com" } }
            };
        IngredientCategories[2].IngredientSubCategories = common.Values.ToList();
        IngredientSubCategories = IngredientSubCategories.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, IngredientSubCategory>()
            {
                { id++, new IngredientSubCategory { Name = "Frutas e Legumes", ImageUrl = "www.frutas&legumes.com" } },
                { id++, new IngredientSubCategory { Name = "Padaria e Pastelaria", ImageUrl = "www.padaria&pastelaria.com" } },
                { id++, new IngredientSubCategory { Name = "Peixaria", ImageUrl = "www.peixaria.com" } },
                { id++, new IngredientSubCategory { Name = "Talho", ImageUrl = "www.talho.com" } },
                { id++, new IngredientSubCategory { Name = "Frutos Secos", ImageUrl = "www.frutossecos.com" } },
                { id++, new IngredientSubCategory { Name = "Conservas", ImageUrl = "www.conservas.com" } }
            };
        IngredientCategories[3].IngredientSubCategories = common.Values.ToList();
        IngredientSubCategories = IngredientSubCategories.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, IngredientSubCategory>()
            {
                { id++, new IngredientSubCategory { Name = "Sobremesa", ImageUrl = "www.sobremesa.com" } },
                { id++, new IngredientSubCategory { Name = "Sopa", ImageUrl = "www.sopa.com" } },
                { id++, new IngredientSubCategory { Name = "Temperos", ImageUrl = "www.temperos.com" } },
                { id++, new IngredientSubCategory { Name = "Farinha", ImageUrl = "www.farinha.com" } },
                { id++, new IngredientSubCategory { Name = "Massa", ImageUrl = "www.massa.com" } },
                { id++, new IngredientSubCategory { Name = "Mel", ImageUrl = "www.mel.com" } }
            };

        IngredientCategories[4].IngredientSubCategories = common.Values.ToList();
        IngredientSubCategories = IngredientSubCategories.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, IngredientSubCategory>()
            {
                { id++, new IngredientSubCategory { Name = "Espumante", ImageUrl = "www.espumante.com" } },
                { id++, new IngredientSubCategory { Name = "Vinho", ImageUrl = "www.vinho.com" } }

            };
        IngredientCategories[5].IngredientSubCategories = common.Values.ToList();
        IngredientSubCategories = IngredientSubCategories.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, IngredientSubCategory>()
            {
                { id++, new IngredientSubCategory { Name = "Leite", ImageUrl = "www.leite.com" } },
                { id++, new IngredientSubCategory { Name = "Ovos", ImageUrl = "www.ovos.com" } },
                { id++, new IngredientSubCategory { Name = "Manteiga", ImageUrl = "www.manteiga.com" } },
                { id++, new IngredientSubCategory { Name = "Natas", ImageUrl = "www.natas.com" } }

            };
        IngredientCategories[6].IngredientSubCategories = common.Values.ToList();
        IngredientSubCategories = IngredientSubCategories.Union(common).ToDictionary(k => k.Key, v => v.Value);

        context.SaveChanges();
    }

    public void SeedIngredientCategories(RestaurantDbContext context)
    {
        IngredientCategories = new Dictionary<int, IngredientCategory>()
            {
                {1, new IngredientCategory { Name =  "Bebidas", ImageUrl = "www.bebidas.com" } },
                {2, new IngredientCategory { Name = "Charcutaria", ImageUrl = "www.charcutaria.com" } },
                {3, new IngredientCategory { Name = "Frescos", ImageUrl = "www.frescos.com" } },
                {4, new IngredientCategory { Name ="Mercearia", ImageUrl = "www.mercearia.com" } },
                {5, new IngredientCategory { Name ="Vinhos", ImageUrl = "www.vinhos.com" } },
                {6, new IngredientCategory { Name ="Lacticínios", ImageUrl = "www.lacticinios.com" } }
            };
        context.IngredientCategories.AddRange(IngredientCategories.Values);

        context.SaveChanges();
    }

    public void SeedCatalog(RestaurantDbContext context)
    {
        int id = 1;
        Dictionary<int, Catalog> common;
        Catalogs = new Dictionary<int, Catalog>();

        common = new Dictionary<int, Catalog>()
            {
                { id++, new Catalog { Name = "Tarte de Quinoa", ImageUrl = "http://i.imgur.com/nqHxrUQ.png", HeroImageUrl = "https://cdn.nomadfoodscdn.com/-/media/project/bluesteel/iglo-pt/receitas/2020/gerador-receitas/tarte-lowcarb-de-quinoa-abobora-e-ervilha-com-salada-de-espinafre-fresco-e-nozes.jpg" } },
                { id++, new Catalog { Name = "Quiche de Legumes", ImageUrl = "http://i.imgur.com/aqNBIvg.png", HeroImageUrl = "https://cdn.vidaativa.pt/uploads/2020/01/quiche-cogumelos-legumes.jpg" } },
                { id++, new Catalog { Name = "Quiche de Salmão", ImageUrl = "http://i.imgur.com/kD3Gai1.png", HeroImageUrl = "https://media-manager.noticiasaominuto.com/1920/1605523869/naom_5fb2566929ea5.jpg?crop_params=eyJsYW5kc2NhcGUiOnsiY3JvcFdpZHRoIjoyNTYwLCJjcm9wSGVpZ2h0IjoxNDQwLCJjcm9wWCI6MCwiY3JvcFkiOjIxfSwicG9ydHJhaXQiOnsiY3JvcFdpZHRoIjo5NTQsImNyb3BIZWlnaHQiOjE2OTYsImNyb3BYIjo3NDYsImNyb3BZIjoxMH19" } },
                { id++, new Catalog { Name = "Tortilhas Verdes", ImageUrl = "http://i.imgur.com/MHgXBK6.png" } },
                { id++, new Catalog { Name = "Tortilha de Milho", ImageUrl = "http://i.imgur.com/AORyj0l.png" } },
                { id++, new Catalog { Name = "Wraps com Pepino e Mousse de Salmão", ImageUrl = "http://i.imgur.com/79ubX5o.png" } },
                { id++, new Catalog { Name = "Wraps de Mandioca", ImageUrl = "http://i.imgur.com/Czu5Jvc.png" } },
                { id++, new Catalog { Name = "Tostas de Queijo", ImageUrl = "http://i.imgur.com/hV4ySZy.png" } }

            };
        Categories[1].Catalogs = common.Values.ToList();
        Catalogs = Catalogs.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, Catalog>()
            {
                { id++, new Catalog { Name = "Panados no Forno", ImageUrl = "http://i.imgur.com/kceE8UD.png", HeroImageUrl = "https://mamapaleo.blogs.nit.pt/wp-content/uploads/2017/06/IMG_8202-1-1300x731.jpg" } },
                { id++, new Catalog { Name = "Jardineira de Perú", ImageUrl = "http://i.imgur.com/ZouoIsy.png", HeroImageUrl = "https://www.saborintenso.com/images/receitas/Jardineira-de-Peru-com-Cogumelos-SI-2.jpg" } },
                { id++, new Catalog { Name = "Peru", ImageUrl = "http://i.imgur.com/GsrXqKt.png", HeroImageUrl = "https://fortissima.com.br/wp-content/uploads/2015/11/peru-de-natal-istock-getty-images1.jpg" } },
                { id++, new Catalog { Name = "Strogonoff", ImageUrl = "http://i.imgur.com/NYHMnBo.png" } },
                { id++, new Catalog { Name = "Borrego em Crosta de Alecrim", ImageUrl = "http://i.imgur.com/rSLdbGK.png" } },
                { id++, new Catalog { Name = "Quiche de Frango", ImageUrl = "http://i.imgur.com/KX1oY9N.png" } },
                { id++, new Catalog { Name = "Frango de Fricassé", ImageUrl = "http://i.imgur.com/6zlSU7b.png" } },
                { id++, new Catalog { Name = "Carne de Porco à Alentejana", ImageUrl = "http://i.imgur.com/cJXvCnK.png" } }

            };
        Categories[2].Catalogs = common.Values.ToList();
        Catalogs = Catalogs.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, Catalog>()
            {
                { id++, new Catalog { Name = "Lombos de Salmão Grelhados", ImageUrl = "http://i.imgur.com/ep15IHE.png", HeroImageUrl = "https://cdn.vidaativa.pt/uploads/2020/01/salmao-grelhado-com-oregaos.jpg" } },
                { id++, new Catalog { Name = "Bacalhau com Natas", ImageUrl = "http://i.imgur.com/iYgAwFI.png", HeroImageUrl = "https://xtudoreceitas.com/wp-content/uploads/Bacalhau-com-Natas-Simples.jpg" } },
                { id++, new Catalog { Name = "Açorda Rica de Marisco", ImageUrl = "http://i.imgur.com/91fjknG.png", HeroImageUrl = "https://www.teleculinaria.pt/wp-content/uploads/2015/04/A%C3%A7orda-de-mariscos-11.jpg" } },
                { id++, new Catalog { Name = "Bacalhau à Bras", ImageUrl = "http://i.imgur.com/Zrj0YHS.png" } },
                { id++, new Catalog { Name = "Carpaccio de Vieiras", ImageUrl = "http://i.imgur.com/1fy55SF.png" } },
                { id++, new Catalog { Name = "Talharim de Vieiras", ImageUrl = "http://i.imgur.com/DC0THKE.png" } },
                { id++, new Catalog { Name = "Filetes de Sardinha", ImageUrl = "http://i.imgur.com/dQvbPjZ.png" } },
                { id++, new Catalog { Name = "Filetes de Peixe-espada", ImageUrl = "http://i.imgur.com/ifM8lax.png" } }
            };
        Categories[3].Catalogs = common.Values.ToList();
        Catalogs = Catalogs.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, Catalog>()
            {
                { id++, new Catalog { Name = "Espumante com Sorbet de Limão", ImageUrl = "http://i.imgur.com/v139jIW.png" } },
                { id++, new Catalog { Name = "Cocktail", ImageUrl = "http://i.imgur.com/QapQTCR.png" } },
                { id++, new Catalog { Name = "Caipirinha", ImageUrl = "http://i.imgur.com/U7Nfg3M.png" } },
                { id++, new Catalog { Name = "Cocktail Tricolor", ImageUrl = "http://i.imgur.com/1joXJXl.png" } }

            };
        Categories[4].Catalogs = common.Values.ToList();
        Catalogs = Catalogs.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, Catalog>()
            {
                { id++, new Catalog { Name = "Brownies de Manteiga de Amendoim", ImageUrl = "http://i.imgur.com/3jSMmtK.png" } },
                { id++, new Catalog { Name = "Cupcakes", ImageUrl = "http://i.imgur.com/RsCIy6t.png" } }

            };
        Categories[5].Catalogs = common.Values.ToList();
        Catalogs = Catalogs.Union(common).ToDictionary(k => k.Key, v => v.Value);

        context.SaveChanges();
    }

    public void SeedCatalogIngredient(RestaurantDbContext context)
    {

        Catalogs[1].Ingredients.Add(Ingredients[80]);
        Catalogs[1].Ingredients.Add(Ingredients[96]);
        Catalogs[1].Ingredients.Add(Ingredients[97]);
        Catalogs[1].Ingredients.Add(Ingredients[77]);
        Catalogs[1].Ingredients.Add(Ingredients[127]);
        Catalogs[1].Ingredients.Add(Ingredients[126]);
        Catalogs[1].Ingredients.Add(Ingredients[125]);
        Catalogs[1].Ingredients.Add(Ingredients[86]);
        Catalogs[1].Ingredients.Add(Ingredients[124]);
        Catalogs[1].Ingredients.Add(Ingredients[123]);
        Catalogs[1].Ingredients.Add(Ingredients[83]);
        Catalogs[1].Ingredients.Add(Ingredients[79]);
        Catalogs[1].Ingredients.Add(Ingredients[121]);
        Catalogs[1].Ingredients.Add(Ingredients[120]);
        Catalogs[2].Ingredients.Add(Ingredients[68]);
        Catalogs[2].Ingredients.Add(Ingredients[119]);
        Catalogs[2].Ingredients.Add(Ingredients[118]);
        Catalogs[2].Ingredients.Add(Ingredients[117]);
        Catalogs[2].Ingredients.Add(Ingredients[116]);
        Catalogs[2].Ingredients.Add(Ingredients[115]);
        Catalogs[2].Ingredients.Add(Ingredients[114]);
        Catalogs[3].Ingredients.Add(Ingredients[113]);
        Catalogs[3].Ingredients.Add(Ingredients[77]);
        Catalogs[3].Ingredients.Add(Ingredients[127]);
        Catalogs[3].Ingredients.Add(Ingredients[98]);
        Catalogs[3].Ingredients.Add(Ingredients[123]);
        Catalogs[3].Ingredients.Add(Ingredients[68]);
        Catalogs[3].Ingredients.Add(Ingredients[79]);
        Catalogs[3].Ingredients.Add(Ingredients[117]);
        Catalogs[3].Ingredients.Add(Ingredients[112]);
        Catalogs[3].Ingredients.Add(Ingredients[86]);
        Catalogs[3].Ingredients.Add(Ingredients[111]);
        Catalogs[4].Ingredients.Add(Ingredients[111]);
        Catalogs[4].Ingredients.Add(Ingredients[110]);
        Catalogs[4].Ingredients.Add(Ingredients[90]);
        Catalogs[4].Ingredients.Add(Ingredients[77]);
        Catalogs[4].Ingredients.Add(Ingredients[68]);
        Catalogs[4].Ingredients.Add(Ingredients[1]);
        Catalogs[4].Ingredients.Add(Ingredients[109]);
        Catalogs[4].Ingredients.Add(Ingredients[108]);
        Catalogs[4].Ingredients.Add(Ingredients[127]);
        Catalogs[4].Ingredients.Add(Ingredients[107]);
        Catalogs[4].Ingredients.Add(Ingredients[98]);
        Catalogs[4].Ingredients.Add(Ingredients[112]);
        Catalogs[4].Ingredients.Add(Ingredients[106]);
        Catalogs[4].Ingredients.Add(Ingredients[105]);
        Catalogs[4].Ingredients.Add(Ingredients[104]);
        Catalogs[5].Ingredients.Add(Ingredients[103]);
        Catalogs[5].Ingredients.Add(Ingredients[123]);
        Catalogs[5].Ingredients.Add(Ingredients[83]);
        Catalogs[5].Ingredients.Add(Ingredients[102]);
        Catalogs[5].Ingredients.Add(Ingredients[101]);
        Catalogs[5].Ingredients.Add(Ingredients[79]);
        Catalogs[5].Ingredients.Add(Ingredients[116]);
        Catalogs[5].Ingredients.Add(Ingredients[100]);
        Catalogs[5].Ingredients.Add(Ingredients[99]);
        Catalogs[5].Ingredients.Add(Ingredients[66]);
        Catalogs[6].Ingredients.Add(Ingredients[113]);
        Catalogs[6].Ingredients.Add(Ingredients[77]);
        Catalogs[6].Ingredients.Add(Ingredients[127]);
        Catalogs[6].Ingredients.Add(Ingredients[83]);
        Catalogs[6].Ingredients.Add(Ingredients[128]);
        Catalogs[6].Ingredients.Add(Ingredients[65]);
        Catalogs[6].Ingredients.Add(Ingredients[126]);
        Catalogs[6].Ingredients.Add(Ingredients[63]);
        Catalogs[6].Ingredients.Add(Ingredients[120]);
        Catalogs[6].Ingredients.Add(Ingredients[98]);
        Catalogs[6].Ingredients.Add(Ingredients[99]);
        Catalogs[7].Ingredients.Add(Ingredients[29]);
        Catalogs[7].Ingredients.Add(Ingredients[77]);
        Catalogs[7].Ingredients.Add(Ingredients[1]);
        Catalogs[7].Ingredients.Add(Ingredients[28]);
        Catalogs[7].Ingredients.Add(Ingredients[83]);
        Catalogs[7].Ingredients.Add(Ingredients[79]);
        Catalogs[7].Ingredients.Add(Ingredients[127]);
        Catalogs[7].Ingredients.Add(Ingredients[86]);
        Catalogs[8].Ingredients.Add(Ingredients[120]);
        Catalogs[8].Ingredients.Add(Ingredients[27]);
        Catalogs[8].Ingredients.Add(Ingredients[126]);
        Catalogs[8].Ingredients.Add(Ingredients[86]);
        Catalogs[8].Ingredients.Add(Ingredients[127]);
        Catalogs[8].Ingredients.Add(Ingredients[107]);
        Catalogs[9].Ingredients.Add(Ingredients[26]);
        Catalogs[9].Ingredients.Add(Ingredients[98]);
        Catalogs[9].Ingredients.Add(Ingredients[25]);
        Catalogs[9].Ingredients.Add(Ingredients[73]);
        Catalogs[9].Ingredients.Add(Ingredients[120]);
        Catalogs[9].Ingredients.Add(Ingredients[70]);
        Catalogs[9].Ingredients.Add(Ingredients[126]);
        Catalogs[9].Ingredients.Add(Ingredients[66]);
        Catalogs[9].Ingredients.Add(Ingredients[24]);
        Catalogs[10].Ingredients.Add(Ingredients[106]);
        Catalogs[10].Ingredients.Add(Ingredients[23]);
        Catalogs[10].Ingredients.Add(Ingredients[22]);
        Catalogs[10].Ingredients.Add(Ingredients[21]);
        Catalogs[10].Ingredients.Add(Ingredients[108]);
        Catalogs[10].Ingredients.Add(Ingredients[95]);
        Catalogs[10].Ingredients.Add(Ingredients[28]);
        Catalogs[10].Ingredients.Add(Ingredients[77]);
        Catalogs[10].Ingredients.Add(Ingredients[127]);
        Catalogs[10].Ingredients.Add(Ingredients[81]);
        Catalogs[10].Ingredients.Add(Ingredients[79]);
        Catalogs[10].Ingredients.Add(Ingredients[19]);
        Catalogs[11].Ingredients.Add(Ingredients[108]);
        Catalogs[11].Ingredients.Add(Ingredients[77]);
        Catalogs[11].Ingredients.Add(Ingredients[18]);
        Catalogs[11].Ingredients.Add(Ingredients[98]);
        Catalogs[11].Ingredients.Add(Ingredients[17]);
        Catalogs[11].Ingredients.Add(Ingredients[107]);
        Catalogs[11].Ingredients.Add(Ingredients[83]);
        Catalogs[11].Ingredients.Add(Ingredients[89]);
        Catalogs[11].Ingredients.Add(Ingredients[30]);
        Catalogs[11].Ingredients.Add(Ingredients[16]);
        Catalogs[11].Ingredients.Add(Ingredients[95]);
        Catalogs[11].Ingredients.Add(Ingredients[14]);
        Catalogs[11].Ingredients.Add(Ingredients[13]);
        Catalogs[11].Ingredients.Add(Ingredients[12]);
        Catalogs[11].Ingredients.Add(Ingredients[11]);
        Catalogs[11].Ingredients.Add(Ingredients[86]);
        Catalogs[11].Ingredients.Add(Ingredients[10]);
        Catalogs[11].Ingredients.Add(Ingredients[9]);
        Catalogs[12].Ingredients.Add(Ingredients[8]);
        Catalogs[12].Ingredients.Add(Ingredients[83]);
        Catalogs[12].Ingredients.Add(Ingredients[95]);
        Catalogs[12].Ingredients.Add(Ingredients[81]);
        Catalogs[12].Ingredients.Add(Ingredients[77]);
        Catalogs[12].Ingredients.Add(Ingredients[127]);
        Catalogs[12].Ingredients.Add(Ingredients[79]);
        Catalogs[12].Ingredients.Add(Ingredients[1]);
        Catalogs[12].Ingredients.Add(Ingredients[7]);
        Catalogs[12].Ingredients.Add(Ingredients[6]);
        Catalogs[13].Ingredients.Add(Ingredients[84]);
        Catalogs[13].Ingredients.Add(Ingredients[98]);
        Catalogs[13].Ingredients.Add(Ingredients[5]);
        Catalogs[13].Ingredients.Add(Ingredients[4]);
        Catalogs[13].Ingredients.Add(Ingredients[20]);
        Catalogs[13].Ingredients.Add(Ingredients[23]);
        Catalogs[13].Ingredients.Add(Ingredients[3]);
        Catalogs[13].Ingredients.Add(Ingredients[95]);
        Catalogs[13].Ingredients.Add(Ingredients[83]);
        Catalogs[13].Ingredients.Add(Ingredients[81]);
        Catalogs[13].Ingredients.Add(Ingredients[2]);
        Catalogs[13].Ingredients.Add(Ingredients[126]);
        Catalogs[13].Ingredients.Add(Ingredients[107]);
        Catalogs[13].Ingredients.Add(Ingredients[73]);
        Catalogs[13].Ingredients.Add(Ingredients[114]);
        Catalogs[14].Ingredients.Add(Ingredients[92]);
        Catalogs[14].Ingredients.Add(Ingredients[68]);
        Catalogs[14].Ingredients.Add(Ingredients[77]);
        Catalogs[14].Ingredients.Add(Ingredients[15]);
        Catalogs[14].Ingredients.Add(Ingredients[26]);
        Catalogs[14].Ingredients.Add(Ingredients[7]);
        Catalogs[14].Ingredients.Add(Ingredients[31]);
        Catalogs[14].Ingredients.Add(Ingredients[86]);
        Catalogs[15].Ingredients.Add(Ingredients[26]);
        Catalogs[15].Ingredients.Add(Ingredients[95]);
        Catalogs[15].Ingredients.Add(Ingredients[126]);
        Catalogs[15].Ingredients.Add(Ingredients[77]);
        Catalogs[15].Ingredients.Add(Ingredients[127]);
        Catalogs[15].Ingredients.Add(Ingredients[1]);
        Catalogs[15].Ingredients.Add(Ingredients[25]);
        Catalogs[15].Ingredients.Add(Ingredients[92]);
        Catalogs[15].Ingredients.Add(Ingredients[86]);
        Catalogs[15].Ingredients.Add(Ingredients[98]);
        Catalogs[15].Ingredients.Add(Ingredients[10]);
        Catalogs[16].Ingredients.Add(Ingredients[32]);
        Catalogs[16].Ingredients.Add(Ingredients[77]);
        Catalogs[16].Ingredients.Add(Ingredients[33]);
        Catalogs[16].Ingredients.Add(Ingredients[20]);
        Catalogs[16].Ingredients.Add(Ingredients[83]);
        Catalogs[16].Ingredients.Add(Ingredients[10]);
        Catalogs[16].Ingredients.Add(Ingredients[81]);
        Catalogs[16].Ingredients.Add(Ingredients[5]);
        Catalogs[17].Ingredients.Add(Ingredients[10]);
        Catalogs[17].Ingredients.Add(Ingredients[62]);
        Catalogs[17].Ingredients.Add(Ingredients[68]);
        Catalogs[17].Ingredients.Add(Ingredients[127]);
        Catalogs[17].Ingredients.Add(Ingredients[83]);
        Catalogs[17].Ingredients.Add(Ingredients[98]);
        Catalogs[17].Ingredients.Add(Ingredients[113]);
        Catalogs[17].Ingredients.Add(Ingredients[77]);
        Catalogs[17].Ingredients.Add(Ingredients[107]);
        Catalogs[17].Ingredients.Add(Ingredients[23]);
        Catalogs[17].Ingredients.Add(Ingredients[1]);
        Catalogs[17].Ingredients.Add(Ingredients[61]);
        Catalogs[17].Ingredients.Add(Ingredients[76]);
        Catalogs[18].Ingredients.Add(Ingredients[74]);
        Catalogs[18].Ingredients.Add(Ingredients[95]);
        Catalogs[18].Ingredients.Add(Ingredients[83]);
        Catalogs[18].Ingredients.Add(Ingredients[23]);
        Catalogs[18].Ingredients.Add(Ingredients[88]);
        Catalogs[18].Ingredients.Add(Ingredients[68]);
        Catalogs[18].Ingredients.Add(Ingredients[92]);
        Catalogs[18].Ingredients.Add(Ingredients[85]);
        Catalogs[18].Ingredients.Add(Ingredients[120]);

        context.SaveChanges();
    }

    public void SeedCatalogInfo(RestaurantDbContext context)
    {
        int id = 1;
        Dictionary<int, CatalogInfo> common;
        CatalogInfos = new Dictionary<int, CatalogInfo>();

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 4.99f } }
            };
        Catalogs[1].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 4.99f } }
            };
        Catalogs[2].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 5.99f } }

            };
        Catalogs[3].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 3.99f } }
            };
        Catalogs[4].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 5.99f } }
            };
        Catalogs[5].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 7.99f } }
            };
        Catalogs[6].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 7.99f } }
            };
        Catalogs[7].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 4.99f } }
            };
        Catalogs[8].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 9.99f } }
            };
        Catalogs[9].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "P", Price = 14.99f, Description = "Pequena" } },
                { id++, new CatalogInfo { Size = "M", Price = 18.99f, Description = "Média" } },
                { id++, new CatalogInfo { Size = "G", Price = 23.99f, Description = "Grande" } }
            };
        Catalogs[10].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "P", Price = 15.99f, Description = "Pequena" } },
                { id++, new CatalogInfo { Size = "M", Price = 20.99f, Description = "Média" } },
                { id++, new CatalogInfo { Size = "G", Price = 25.99f, Description = "Grande" } }
            };
        Catalogs[11].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 13.99f } }
            };
        Catalogs[12].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "P", Price = 17.99f, Description = "Pequena" } },
                { id++, new CatalogInfo { Size = "M", Price = 24.99f, Description = "Média" } },
                { id++, new CatalogInfo { Size = "G", Price = 30.99f, Description = "Grande" } }
            };
        Catalogs[13].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "P", Price = 9.99f, Description = "Pequena" } },
                { id++, new CatalogInfo { Size = "M", Price = 13.99f, Description = "Média" } },
                { id++, new CatalogInfo { Size = "G", Price = 16.99f, Description = "Grande" } }
            };
        Catalogs[14].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 14.99f } }
            };
        Catalogs[15].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 18.99f } }
            };
        Catalogs[16].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 19.99f } }
            };
        Catalogs[17].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 16.99f } }
            };
        Catalogs[18].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 24.99f } }
            };
        Catalogs[19].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "P", Price = 9.99f, Description = "Pequena" } },
                { id++, new CatalogInfo { Size = "G", Price = 18.99f, Description = "Média" } }
            };
        Catalogs[20].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "P", Price = 9.99f, Description = "Pequena" } },
                { id++, new CatalogInfo { Size = "M", Price = 14.99f, Description = "Média" } },
                { id++, new CatalogInfo { Size = "G", Price = 18.99f, Description = "Grande" } }
            };
        Catalogs[21].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 13.99f } }
            };
        Catalogs[22].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "P", Price = 8.99f, Description = "Pequena" } },
                { id++, new CatalogInfo { Size = "M", Price = 14.99f, Description = "Média" } }
            };
        Catalogs[23].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 3.99f } }
            };
        Catalogs[24].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 3.50f } }
            };
        Catalogs[25].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 5.99f } }
            };
        Catalogs[26].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 5.50f } }
            };
        Catalogs[27].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 7.99f } }
            };
        Catalogs[28].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 5.99f } }
            };
        Catalogs[29].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        common = new Dictionary<int, CatalogInfo>()
            {
                { id++, new CatalogInfo { Size = "Normal", Price = 4.99f } }
            };
        Catalogs[30].CatalogInfos = common.Values.ToList();
        CatalogInfos = CatalogInfos.Union(common).ToDictionary(k => k.Key, v => v.Value);

        context.SaveChanges();
    }

    public void SeedIngredients(RestaurantDbContext context)
    {
        Ingredients = new Dictionary<int, Ingredient>()
        {
                { 1, new Ingredient { Name = "Água", ImageUrl = "www.agua.com", IngredientSubCategoryId = IngredientSubCategories[1].Id } },
                { 2, new Ingredient { Name = "Água com Gás", ImageUrl = "www.agua&gas.com", IngredientSubCategoryId = IngredientSubCategories[1].Id } },
                { 3, new Ingredient { Name = "Água Tónica ", ImageUrl = "www.agua&tonica.com", IngredientSubCategoryId = IngredientSubCategories[1].Id } },
                { 4, new Ingredient { Name = "Farinha Integral", ImageUrl = "www.farinha&integral.com", IngredientSubCategoryId = IngredientSubCategories[19].Id } },
                { 5, new Ingredient { Name = "Cacau em Pó", ImageUrl = "www.cacau&po.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 6, new Ingredient { Name = "Fermento em Pó", ImageUrl = "www.fermento&po.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 7, new Ingredient { Name = "Canela ", ImageUrl = "www.canela.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 8, new Ingredient { Name = "Óleo", ImageUrl = "www.oleo.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 9, new Ingredient { Name = "Banana", ImageUrl = "www.banana.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 10, new Ingredient { Name = "Ovo", ImageUrl = "www.ovo.com", IngredientSubCategoryId = IngredientSubCategories[25].Id } },
                { 11, new Ingredient { Name = "Leite", ImageUrl = "www.leite.com", IngredientSubCategoryId = IngredientSubCategories[24].Id } },
                { 12, new Ingredient { Name = "Borrego", ImageUrl = "www.borrego.com", IngredientSubCategoryId = IngredientSubCategories[11].Id } },
                { 13, new Ingredient { Name = "Alho", ImageUrl = "www.alho.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 14, new Ingredient { Name = "Azeite", ImageUrl = "www.azeite.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 15, new Ingredient { Name = "Cebola", ImageUrl = "www.cebola.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 16, new Ingredient { Name = "Folha de Louro", ImageUrl = "www.folha&louro.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 17, new Ingredient { Name = "Tomate", ImageUrl = "www.tomate.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 18, new Ingredient { Name = "Favas", ImageUrl = "www.favas.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 19, new Ingredient { Name = "Sal", ImageUrl = "www.sal.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 20, new Ingredient { Name = "Vinagre", ImageUrl = "www.vinagre.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 21, new Ingredient { Name = "Coentros", ImageUrl = "www.coentros.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 22, new Ingredient { Name = "Bacalhau", ImageUrl = "www.bacalhau.com", IngredientSubCategoryId = IngredientSubCategories[12].Id } },
                { 23, new Ingredient { Name = "Pão", ImageUrl = "www.pao.com", IngredientSubCategoryId = IngredientSubCategories[11].Id } },
                { 24, new Ingredient { Name = "Marshmallow", ImageUrl = "www.marshmallow.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } },
                { 25, new Ingredient { Name = "Amendoim", ImageUrl = "www.amendoim.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } },
                { 26, new Ingredient { Name = "Coco", ImageUrl = "www.coco.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } },
                { 27, new Ingredient { Name = "Chocolate Preto", ImageUrl = "www.chocolate&preto.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } },
                { 28, new Ingredient { Name = "Manteiga", ImageUrl = "www.manteiga.com", IngredientSubCategoryId = IngredientSubCategories[26].Id } },
                { 29, new Ingredient { Name = "Chocolate Branco", ImageUrl = "www.chocolate&branco.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } },
                { 30, new Ingredient { Name = "Quinoa", ImageUrl = "www.quinoa.com", IngredientSubCategoryId = IngredientSubCategories[19].Id } },
                { 31, new Ingredient { Name = "Farinha de amêndoa", ImageUrl = "www.farinhadeamendoa.com", IngredientSubCategoryId = IngredientSubCategories[19].Id } },
                { 32, new Ingredient { Name = "Amido de milho", ImageUrl = "www.amidodemilho.com", IngredientSubCategoryId = IngredientSubCategories[19].Id } },
                { 33, new Ingredient { Name = "Limão", ImageUrl = "www.limao.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 34, new Ingredient { Name = "Pimenta", ImageUrl = "www.pimenta.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 35, new Ingredient { Name = "Vaqueiro Líquido", ImageUrl = "www.vaqueiroliquido.com", IngredientSubCategoryId = IngredientSubCategories[25].Id } },
                { 36, new Ingredient { Name = "Sopa de Vaqueiro", ImageUrl = "www.sopavaqueiro.com", IngredientSubCategoryId = IngredientSubCategories[19].Id } },
                { 37, new Ingredient { Name = "Pesto", ImageUrl = "www.pesto.com", IngredientSubCategoryId = IngredientSubCategories[19].Id } },
                { 38, new Ingredient { Name = "Alho Francês", ImageUrl = "www.alhofrances.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 39, new Ingredient { Name = "Espargo", ImageUrl = "www.espargo.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 40, new Ingredient { Name = "Manjericão", ImageUrl = "www.manjericao.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 41, new Ingredient { Name = "Queijo", ImageUrl = "www.queijo.com", IngredientSubCategoryId = IngredientSubCategories[8].Id } },
                { 42, new Ingredient { Name = "Legumes em cubinhos", ImageUrl = "www.legumescubinhos.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 43, new Ingredient { Name = "Knorr Natura Legumes", ImageUrl = "www.naturalegumes.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 44, new Ingredient { Name = "Massa Refrigerada", ImageUrl = "www.massarefrigerada.com", IngredientSubCategoryId = IngredientSubCategories[20].Id } },
                { 45, new Ingredient { Name = "Queijo Mozzarella", ImageUrl = "www.queijomozzarella.com", IngredientSubCategoryId = IngredientSubCategories[8].Id } },
                { 46, new Ingredient { Name = "Sopa de Sementes de Sésano", ImageUrl = "www.sopasesamo.com", IngredientSubCategoryId = IngredientSubCategories[17].Id } },
                { 47, new Ingredient { Name = "Amêndoa", ImageUrl = "www.amendoa.com", IngredientSubCategoryId = IngredientSubCategories[14].Id } },
                { 48, new Ingredient { Name = "Salmão", ImageUrl = "www.salmao.com", IngredientSubCategoryId = IngredientSubCategories[12].Id } },
                { 49, new Ingredient { Name = "Pimento Amarelo", ImageUrl = "www.pimentoamarelo.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 50, new Ingredient { Name = "Espinafres", ImageUrl = "www.espinafres.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 51, new Ingredient { Name = "Farinha", ImageUrl = "www.farinha.com", IngredientSubCategoryId = IngredientSubCategories[19].Id } },
                { 52, new Ingredient { Name = "Caril em Pó", ImageUrl = "www.carilpo.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 53, new Ingredient { Name = "Peru", ImageUrl = "www.peru.com", IngredientSubCategoryId = IngredientSubCategories[13].Id } },
                { 54, new Ingredient { Name = "Mel", ImageUrl = "www.mel.com", IngredientSubCategoryId = IngredientSubCategories[21].Id } },
                { 55, new Ingredient { Name = "Cenoura", ImageUrl = "www.cenoura.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 56, new Ingredient { Name = "Couve Roxa", ImageUrl = "www.couveroxa.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 57, new Ingredient { Name = "Maionese", ImageUrl = "www.maionese.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 58, new Ingredient { Name = "Cebola Doce", ImageUrl = "www.ceboladoce.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 59, new Ingredient { Name = "Atum", ImageUrl = "www.atum.com", IngredientSubCategoryId = IngredientSubCategories[12].Id } },
                { 60, new Ingredient { Name = "Ketchup", ImageUrl = "www.ketchup.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 61, new Ingredient { Name = "Picles", ImageUrl = "www.picles.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 62, new Ingredient { Name = "Tortilha", ImageUrl = "www.tortilha.com", IngredientSubCategoryId = IngredientSubCategories[19].Id } },
                { 63, new Ingredient { Name = "Alface", ImageUrl = "www.alface.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 64, new Ingredient { Name = "Açúcar", ImageUrl = "www.açucar.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 65, new Ingredient { Name = "Vinagre de Sidra", ImageUrl = "www.vinagresidra.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 66, new Ingredient { Name = "Pepino", ImageUrl = "www.pepino.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 67, new Ingredient { Name = "Farinha de Mandioca", ImageUrl = "www.farinhamandioca.com", IngredientSubCategoryId = IngredientSubCategories[19].Id } },
                { 68, new Ingredient { Name = "Linguiça", ImageUrl = "www.linguiça.com", IngredientSubCategoryId = IngredientSubCategories[13].Id } },
                { 69, new Ingredient { Name = "Oregãos", ImageUrl = "www.oregaos.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 70, new Ingredient { Name = "Frango", ImageUrl = "www.frango.com", IngredientSubCategoryId = IngredientSubCategories[13].Id } },
                { 71, new Ingredient { Name = "Caldo de Galinha Granulado", ImageUrl = "www.galinhagranulado.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 72, new Ingredient { Name = "Meloa", ImageUrl = "www.meloa.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 73, new Ingredient { Name = "Batata", ImageUrl = "www.batata.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 74, new Ingredient { Name = "Abóbora", ImageUrl = "www.abobora.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 75, new Ingredient { Name = "Curgete", ImageUrl = "www.curgete.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 76, new Ingredient { Name = "Vinho Branco", ImageUrl = "www.vinhobranco.com", IngredientSubCategoryId = IngredientSubCategories[23].Id } },
                { 77, new Ingredient { Name = "Feijão Verde", ImageUrl = "www.feijaoverde.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 78, new Ingredient { Name = "Laranja", ImageUrl = "www.laranja.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 79, new Ingredient { Name = "Vinagre de Framboesa", ImageUrl = "www.vinagreframboesa.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 80, new Ingredient { Name = "Curcuma", ImageUrl = "www.curcuma.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 81, new Ingredient { Name = "Salsicha", ImageUrl = "www.salsicha.com", IngredientSubCategoryId = IngredientSubCategories[5].Id } },
                { 82, new Ingredient { Name = "Broa de Milho", ImageUrl = "www.broamilho.com", IngredientSubCategoryId = IngredientSubCategories[11].Id } },
                { 83, new Ingredient { Name = "Tâmara", ImageUrl = "www.tamara.com", IngredientSubCategoryId = IngredientSubCategories[12].Id } },
                { 84, new Ingredient { Name = "Azeitonas", ImageUrl = "www.azeitonas.com", IngredientSubCategoryId = IngredientSubCategories[15].Id } },
                { 85, new Ingredient { Name = "Malagueta", ImageUrl = "www.malagueta.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 86, new Ingredient { Name = "Salsa", ImageUrl = "www.salsa.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 87, new Ingredient { Name = "Hortelã", ImageUrl = "www.hortela.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 88, new Ingredient { Name = "Vaca", ImageUrl = "www.vaca.com", IngredientSubCategoryId = IngredientSubCategories[13].Id } },
                { 89, new Ingredient { Name = "Cogumelos", ImageUrl = "www.cogumelos.com", IngredientSubCategoryId = IngredientSubCategories[15].Id } },
                { 90, new Ingredient { Name = "Vinho Madeira", ImageUrl = "www.vinhomadeira.com", IngredientSubCategoryId = IngredientSubCategories[23].Id } },
                { 91, new Ingredient { Name = "Massa de Pimentão", ImageUrl = "www.massapimentao.com", IngredientSubCategoryId = IngredientSubCategories[20].Id } },
                { 92, new Ingredient { Name = "Alecrim", ImageUrl = "www.alecrim.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 93, new Ingredient { Name = "Batata Doce", ImageUrl = "www.batatadoce.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 94, new Ingredient { Name = "Colorau", ImageUrl = "www.colorau.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 95, new Ingredient { Name = "Bacon", ImageUrl = "www.bacon.com", IngredientSubCategoryId = IngredientSubCategories[5].Id } },
                { 96, new Ingredient { Name = "Iogurte Natural", ImageUrl = "www.iogurtenatural.com", IngredientSubCategoryId = IngredientSubCategories[24].Id } },
                { 97, new Ingredient { Name = "Amêijoas", ImageUrl = "www.amêijoas.com", IngredientSubCategoryId = IngredientSubCategories[12].Id } },
                { 98, new Ingredient { Name = "Porco", ImageUrl = "www.porco.com", IngredientSubCategoryId = IngredientSubCategories[13].Id } },
                { 99, new Ingredient { Name = "Aneto", ImageUrl = "www.aneto.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 100, new Ingredient { Name = "Brócolos", ImageUrl = "www.brocolos.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 101, new Ingredient { Name = "Mexilhões", ImageUrl = "www.mexilhoes.com", IngredientSubCategoryId = IngredientSubCategories[12].Id } },
                { 102, new Ingredient { Name = "Lagosta", ImageUrl = "www.lagosta.com", IngredientSubCategoryId = IngredientSubCategories[12].Id } },
                { 103, new Ingredient { Name = "Camarões", ImageUrl = "www.camarões.com", IngredientSubCategoryId = IngredientSubCategories[12].Id } },
                { 104, new Ingredient { Name = "Margarina", ImageUrl = "www.margarina.com", IngredientSubCategoryId = IngredientSubCategories[25].Id } },
                { 105, new Ingredient { Name = "Vieira", ImageUrl = "www.vieira.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 106, new Ingredient { Name = "Gengibre", ImageUrl = "www.gengibre.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 107, new Ingredient { Name = "Massa Talharim", ImageUrl = "www.massatalahrim.com", IngredientSubCategoryId = IngredientSubCategories[20].Id } },
                { 108, new Ingredient { Name = "Sardinha", ImageUrl = "www.sardinha.com", IngredientSubCategoryId = IngredientSubCategories[12].Id } },
                { 109, new Ingredient { Name = "Pimento Vermelho", ImageUrl = "www.pimentovermelho.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 110, new Ingredient { Name = "Pimento Verde", ImageUrl = "www.pimentoverde.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 111, new Ingredient { Name = "Filetes de Espada", ImageUrl = "www.filetesespada.com", IngredientSubCategoryId = IngredientSubCategories[12].Id } },
                { 112, new Ingredient { Name = "Caldo de Peixe", ImageUrl = "www.caldopeixe.com", IngredientSubCategoryId = IngredientSubCategories[18].Id } },
                { 113, new Ingredient { Name = "Sorbet de Limão", ImageUrl = "www.sorbetlimao.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } },
                { 114, new Ingredient { Name = "Espumante", ImageUrl = "www.espumante.com", IngredientSubCategoryId = IngredientSubCategories[2].Id } },
                { 115, new Ingredient { Name = "Sorbet de Manga", ImageUrl = "www.sorbetmanda.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } },
                { 116, new Ingredient { Name = "Ice Tea Frutos Exóticos", ImageUrl = "www.icetearfrutosexoticos.com", IngredientSubCategoryId = IngredientSubCategories[3].Id } },
                { 117, new Ingredient { Name = "Rum", ImageUrl = "www.rum.com", IngredientSubCategoryId = IngredientSubCategories[2].Id } },
                { 118, new Ingredient { Name = "Canhaça", ImageUrl = "www.canhaça.com", IngredientSubCategoryId = IngredientSubCategories[2].Id } },
                { 119, new Ingredient { Name = "Limas", ImageUrl = "www.limas.com", IngredientSubCategoryId = IngredientSubCategories[10].Id } },
                { 120, new Ingredient { Name = "Pisang Ambon", ImageUrl = "www.pisanganbon.com", IngredientSubCategoryId = IngredientSubCategories[2].Id } },
                { 121, new Ingredient { Name = "Sumo de Laranja", ImageUrl = "www.sumolaranja.com", IngredientSubCategoryId = IngredientSubCategories[3].Id } },
                { 123, new Ingredient { Name = "Gasosa", ImageUrl = "www.gasosa.com", IngredientSubCategoryId = IngredientSubCategories[1].Id } },
                { 124, new Ingredient { Name = "Gelo", ImageUrl = "www.gelo.com", IngredientSubCategoryId = IngredientSubCategories[1].Id } },
                { 125, new Ingredient { Name = "Chocolate de Leite", ImageUrl = "www.chocolateleite.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } },
                { 126, new Ingredient { Name = "Café", ImageUrl = "www.cafe.com", IngredientSubCategoryId = IngredientSubCategories[4].Id } },
                { 127, new Ingredient { Name = "Cacau", ImageUrl = "www.cacau.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } },
                { 128, new Ingredient { Name = "Creme de Avelãs", ImageUrl = "www.cremeavelas.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } },
                { 129, new Ingredient { Name = "Manteiga de Amendoim", ImageUrl = "www.manteigaamendoim.com", IngredientSubCategoryId = IngredientSubCategories[25].Id } },
                { 130, new Ingredient { Name = "Baunilha", ImageUrl = "www.baunilha.com", IngredientSubCategoryId = IngredientSubCategories[16].Id } }
            };

        context.Ingredients.AddRange(Ingredients.Values);

        context.SaveChanges();
    }
}
