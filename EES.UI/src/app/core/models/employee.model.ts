export interface EmployeeDto {
  id: number;
  fullName: string;
  email: string;
  jobTitle: string;
  hireDate: Date;
}

export interface PaginatedList<T> {
  items: T[];
  pageNumber: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface CreateEmployeeCommand {
  firstName: string;
  lastName: string;
  email: string;
  jobTitle: string;
  hireDate: string; // ISO String
}

export interface UpdateEmployeeCommand extends CreateEmployeeCommand {
  id: number;
}