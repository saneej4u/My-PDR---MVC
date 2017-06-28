import { RouterModule }                 from '@angular/router';

import { AccountComponent }             from './account.component';
import { SignUpComponent }              from './sign-up.component';


export const accountRouting = RouterModule.forChild([
    {
        path: 'login',
        component: AccountComponent
    },
    {
        path:'sign-up',
        component:SignUpComponent
    }
]);