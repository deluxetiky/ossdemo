export class TaskElementModel {
  id: string;
  name: string;
  explanation: string;
  favoriteCount: number;
  createdBy: string;
  lastUpdatedTime: Date;
  isUpdated = false;
}
