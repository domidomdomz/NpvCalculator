import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'percentageFormat'
})
export class PercentageFormatPipe implements PipeTransform {
  transform(value: number): string {
    if (value === null || value === undefined) return '';
    return `${value.toFixed(2)}%`;
  }
}
