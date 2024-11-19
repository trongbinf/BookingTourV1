export interface CreateTour {
  tourName: string;
  description: string;
  city: string;
  country: string;
  duration: string;
  price: number;
  personNumber: number;
  isFullDay: boolean;
  status: boolean;
  categoryId: number;  
}

