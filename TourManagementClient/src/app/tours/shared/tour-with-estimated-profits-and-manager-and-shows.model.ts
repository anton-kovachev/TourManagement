import { TourWithEstimatedProfitsAndManager } from './tour-with-estimated-profits-and-manager.model';
import { Show } from './show.model';

export class TourWithEstimatedProfitsAndManagerAndShows extends TourWithEstimatedProfitsAndManager {
    shows: Show[];
}
