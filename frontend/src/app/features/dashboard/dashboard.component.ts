import { ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ApiService } from '../../core/api.service';
import { DashboardData } from '../../shared/models';

@Component({
  selector: 'app-dashboard',
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    @let d = data();
    @if (error()) {
      <div class="card error">Error: {{ error() }}</div>
    } @else if (!d) {
      <div class="card loading">Loading dashboard…</div>
    } @else {
      <div>
        <h1>Dashboard</h1>
        <div class="grid grid-3">
          <div class="card">
            <h2>Organizations</h2>
            <p>{{ d.totalOrganizations }}</p>
          </div>
          <div class="card">
            <h2>Users</h2>
            <p>{{ d.totalUsers }}</p>
          </div>
          <div class="card">
            <h2>Projects</h2>
            <p>{{ d.totalProjects }} ({{ d.activeProjects }} active)</p>
          </div>
          <div class="card">
            <h2>Time entries</h2>
            <p>{{ d.totalTimeEntries }}</p>
          </div>
          <div class="card">
            <h2>Total invoiced</h2>
            <p>\${{ d.totalInvoiced.toLocaleString() }}</p>
          </div>
        </div>
      </div>
    }
  `
})
export class DashboardComponent implements OnInit {
  private api = inject(ApiService);
  private destroyRef = inject(DestroyRef);

  data = signal<DashboardData | null>(null);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.api.getDashboard()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (data) => this.data.set(data),
        error: (err) => this.error.set(err.message ?? 'Unknown error')
      });
  }
}
