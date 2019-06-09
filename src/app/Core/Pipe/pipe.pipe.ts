import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'search'
})
export class PipePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    if (args == undefined) {
      return value;
    }
    if (!value) return null;
    console.log(value.result);
    
    if (!args) return value.result;

    console.log('in pipe', value.result, args);

    return value.result.filter(Array => Array.title.toLowerCase().indexOf(args.toLowerCase()) !== -1 || Array.description.toLowerCase().toLowerCase().indexOf(args.toLowerCase()) !== -1)
}
}