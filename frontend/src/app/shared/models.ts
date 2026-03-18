export interface DashboardData {
  totalOrganizations: number;
  totalUsers: number;
  totalProjects: number;
  activeProjects: number;
  totalTimeEntries: number;
  totalInvoiced: number;
}

export interface Organization {
  id: string;
  name: string;
  industry: string;
  tier: 'enterprise' | 'professional' | 'starter';
  contactEmail: string;
}

export interface OrganizationSummary {
  organization: Organization;
  projectCount: number;
  userCount: number;
  totalInvoiced: number;
  currency: string;
}

export interface Project {
  id: string;
  name: string;
  status: 'active' | 'completed' | 'draft';
  budgetHours: number;
  startDate: string | null;
  endDate: string | null;
}

export interface ProjectDetail extends Project {
  totalHoursLogged: number | null;
  organization: { id: string; name: string } | null;
}
