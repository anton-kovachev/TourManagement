import { TourWithEstimatedProfitsForUpdate } from './tour-with-estimated-profits-for-update.model';
import { ShowForUpdate } from './show-for-update.model';

export class TourWithEstimatedProfitsAndManagerAndShowsForUpdate extends TourWithEstimatedProfitsForUpdate {
    managerId: string;
    shows: ShowForUpdate[];
}

