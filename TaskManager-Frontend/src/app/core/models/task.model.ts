export interface TaskItem {
  id?: number; // The question mark means it's optional because the server generates it.
  title: string;
  description: string;
  isCompleted: boolean;
  isDeleted: boolean; // The flag we added for soft deletion.
  createdAt: Date; // Creation date for sorting.
  userId: string;
}
