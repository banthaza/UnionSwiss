import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'percentInt'
})
export class PercentIntPipe implements PipeTransform {

  transform(value: number, args?: any): string {
    return `${value.toString()} %`;
  }

}
