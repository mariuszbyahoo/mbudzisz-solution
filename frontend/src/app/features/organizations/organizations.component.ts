import { ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ApiService } from '../../core/api.service';
import { Organization } from '../../shared/models';

@Component({
  selector: 'app-organizations',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [RouterLink],
  template: `
    @if (error()) {
      <div class="card error">Error: {{ error() }}</div>
    } @else if (!list()) {
      <div class="card loading">Loading…</div>
    } @else {
      <div>
        <h1>Organizations</h1>
        <div class="grid grid-2">
          @for (org of list(); track org.id) {
            <a [routerLink]="['/organizations', org.id]" class="card-link">
              <div class="card">
                <h2>{{ org.name }}</h2>
                <p>{{ org.industry }} · <span class="badge badge--{{ org.tier }}">{{ org.tier }}</span></p>
                <p>{{ org.contactEmail }}</p>
              </div>
            </a>
          }
        </div>
      </div>
    }
  `
})
export class OrganizationsComponent implements OnInit {
  private api = inject(ApiService);
  private destroyRef = inject(DestroyRef);

  list = signal<Organization[] | null>(null);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.api.getOrganizations()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (list) => this.list.set(list),
        error: (err) => this.error.set(err.message ?? 'Unknown error')
      });
  }
}
