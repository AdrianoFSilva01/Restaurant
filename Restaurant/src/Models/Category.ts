import Catalog from "./Catalog";

export default class Category {
    id!: number;
    name!: string;
    imageUrl!: string;
    heroImageUrl!: string;
    catalogs!: Array<Catalog>;
}