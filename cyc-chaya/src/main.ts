import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import 'chartjs-adapter-date-fns';
import 'chartjs-adapter-moment';
import 'chartjs-adapter-luxon';


platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
