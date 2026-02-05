/**
 * Data Transfer Object representing an Employee's public data.
 * Used for displaying information in tables and lists.
 */
export interface EmployeeDto {
  id: number;
  fullName: string;
  email: string;
  jobTitle: string;
  hireDate: Date;
}

/**
 * Generic wrapper for paginated server responses.
 * @template T The type of items contained in the list.
 */
export interface PaginatedList<T> {
  /** Array of data items for the current page */
  items: T[];
  pageNumber: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

/**
 * Payload structure for creating a new employee record.
 * Matches the backend 'CreateEmployeeCommand' validation schema.
 */
export interface CreateEmployeeCommand {
  firstName: string;
  lastName: string;
  email: string;
  jobTitle: string;
  /** Represented as ISO String for correct JSON serialization */
  hireDate: string; 
}

/**
 * Payload structure for updating an existing employee.
 * Extends the create command to include the required identifier.
 */
export interface UpdateEmployeeCommand extends CreateEmployeeCommand {
  /** Unique identifier required to match the URL ID in the PUT request */
  id: number;
}