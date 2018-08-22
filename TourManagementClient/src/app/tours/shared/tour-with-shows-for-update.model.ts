import { TourForUpdate } from './tour-for-update.model';
import { Show } from './show.model';

export class TourWithShowsForUpdate extends TourForUpdate {
    shows: Show[];
}
