import { ProductImages }  from './product-images';

export interface Product {
    productID: number,
    name: string,
    description: string,

    unitPrice: number,
    unitsInStock: number,
    color: string,
    brand: string,
    size: string,
    width: number,
    height: number,
    depth: number,

    isAvailableForDelivery: boolean,
    isAvailableOnline: boolean,
    productTypeID: number,

    dateTimeCreated: any,

    additionalFeatures1: string,
    additionalFeatures2: string,
    additionalFeatures3: string,
    productImages:ProductImages[]

}

