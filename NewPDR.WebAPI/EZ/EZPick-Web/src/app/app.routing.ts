import { RouterModule }         from '@angular/router';
import { HomeComponent }        from './home.component';


export const routing = RouterModule.forRoot([
    {
        path: 'home',
        component: HomeComponent
    },
     {
        path: '',
        component: HomeComponent
    }
]);