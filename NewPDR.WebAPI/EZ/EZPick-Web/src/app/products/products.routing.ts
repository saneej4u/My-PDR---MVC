import { RouterModule }                 from '@angular/router';


import { ProductTypesPageComponent }    from './product-types-page.component';
import { ProductsPageComponent }        from './products-page.component';
import { PageDetailsPageComponent }     from './product-details-page.component';

export const productsRouting = RouterModule.forChild([
    {
        path: 'product-types/:id',
        component: ProductTypesPageComponent
    },
    {
        path:'products/:id',
        component:ProductsPageComponent
    },
    {
        path:'product-details/:id',
        component:PageDetailsPageComponent
    }

]);