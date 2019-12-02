import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})
export class ErrorHandlerService {

    constructor(private toastr: ToastrService) { }

    handleError(error?: any): void {
        if (error) {
            if (error.error) {
                if (error.error.message) {
                    this.toastr.error(error.error.message);
                } else if (error.error.title) {
                    this.toastr.error(error.error.title);
                }
            } else {
                this.toastr.error(error.message);
            }
        }
    }
}
