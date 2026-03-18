import { ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { RouterLink, ActivatedRoute } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ApiService } from '../../core/api.service';
import { OrganizationSummary } from '../../shared/models';

@Component({
  selector: 'app-organization-detail',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [RouterLink],
  template: `
    @let s = summary();
    @if (error()) {
      <div class="card error">Error: {{ error() }}</div>
    } @else if (!s) {
      <div class="card loading">Loading…</div>
    } @else {
      <div>
        <p><a routerLink="/organizations">← Organizations</a></p>
        <h1>{{ s.organization.name }}</h1>
        <div class="card">
          <p><strong>Industry:</strong> {{ s.organization.industry }}</p>
          <p><strong>Tier:</strong> <span class="badge badge--{{ s.organization.tier }}">{{ s.organization.tier }}</span></p>
          <p><strong>Contact:</strong> {{ s.organization.contactEmail }}</p>
        </div>
        <div class="grid grid-3">
          <div class="card">
            <h2>Projects</h2>
            <p>{{ s.projectCount }}</p>
          </div>
          <div class="card">
            <h2>Users</h2>
            <p>{{ s.userCount }}</p>
          </div>
          <div class="card">
            <h2>Total invoiced</h2>
            <p>{{ s.currency }} {{ s.totalInvoiced.toLocaleString() }}</p>
          </div>
        </div>
      </div>
    }
  `
})
export class OrganizationDetailComponent implements OnInit {
  private api = inject(ApiService);
  private route = inject(ActivatedRoute);
  private destroyRef = inject(DestroyRef);

  summary = signal<OrganizationSummary | null>(null);
  error = signal<string | null>(null);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.api.getOrganizationSummary(id)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (summary) => this.summary.set(summary),
        error: (err) => this.error.set(err.message ?? 'Unknown error')
      });
  }
}
