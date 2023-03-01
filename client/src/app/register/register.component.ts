import { outputAst } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter(); 
  model: any = {};
  constructor(private accountService: AccountService, 
    private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  register(){
    this.accountService.register(this.model)
    .subscribe({
      next: () => {
        this.cancel();
      }, error: (error) =>{
        let errors = Array.of(error.error.errors);
        if(errors.length !== 0){
          errors.forEach((error: String )=> {
            this.toastrService.error(error[0]);
          })
        }
      }
    });
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
