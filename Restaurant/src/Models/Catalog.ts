import Ingredient from "./Ingredient";

export default class Catalog {
    id!: number;
    name!: string;
    imageUrl!: string;
    heroImageUrl!: string;
    haveIngredients!: boolean;
    catalogInfos!: Array<CatalogInfo>;
    ingredients!: Array<Ingredient>
}

export class CatalogInfo {
    size!: string;
    price!: number;
    description!: string;
}