import { Injectable } from '@angular/core';
import { BASE_URL } from '../../../app.config';
import { HttpClient } from '@angular/common/http';
import { CretaReview } from '../model/create-review.model';
import { catchError, map, Observable, of } from 'rxjs';
import { Review } from '../model/review.model';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private apiUrl = `${BASE_URL}/Review`;
  constructor(private http: HttpClient) { }

  insert(review: CretaReview): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/add`, review);
  }

  getTotalRating(tourId: number): Observable<{ totalReviewsCount: number }> {
    return this.http.get<{ totalReviewsCount: number }>(`${this.apiUrl}/total-rating?tourId=${tourId}`);
  }

  getReviewsById(id: number): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiUrl}/${id}`);
  }

  getAverageRating(tourId: number): Observable<{ averageRating: number }> {
    return this.http.get<{ averageRating: number }>(`${this.apiUrl}/average-rating?tourId=${tourId}`);
  }
  getReviewsByTourId(tourId: number): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiUrl}/reviews-by-tour/${tourId}`);
  }

  checkIfReviewed(bookingId: number): Observable<boolean> {
    return this.http.get<boolean>(`${this.apiUrl}/check-reviewed/${bookingId}`).pipe(
      catchError(() => of(false))
    );
  }


}
