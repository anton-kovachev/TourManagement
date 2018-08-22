import { Show } from '../shows/shared/show.model';
import { TourAstractBase } from './tour-astract-base.model';

export class Tour extends TourAstractBase {
    tourId: string;
    band: string;
}
