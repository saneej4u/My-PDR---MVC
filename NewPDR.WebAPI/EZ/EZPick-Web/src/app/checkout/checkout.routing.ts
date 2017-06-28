import { RouterModule }                 from '@angular/router';

import { CheckoutComponent }            from './checkout.component';
import { ReviewPayComponent }           from './review-pay.component';
import { OrderPayComponent }            from './order-pay.component';
import { OrderConfirmationComponent } from './order-confirmation.component';

import { AuthGuard }                   from '../shared/auth-guard.service';


export const checkoutRouting = RouterModule.forChild([
    {
        path: 'checkout',
        component: CheckoutComponent
    },
    {
        path: 'review-pay',
        component: ReviewPayComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'order-pay',
        component: OrderPayComponent,
        canActivate: [AuthGuard]

    },
    {
        path: 'order-confirmation',
        component: OrderConfirmationComponent,
        canActivate: [AuthGuard]

    }

]);