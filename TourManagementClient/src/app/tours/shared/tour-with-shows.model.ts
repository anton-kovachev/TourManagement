import { Show } from './show.model';
import { Tour } from './tour.model';

export class TourWithShows extends Tour {
    shows: Show[];
}
