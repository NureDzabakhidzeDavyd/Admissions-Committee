import { DynamicFilter } from 'src/app/models/api-request/dynamic-filter/dynamicFilter';

export const getParams = (filters: DynamicFilter[]): URLSearchParams => {
  const queryParams = new URLSearchParams();
  filters.forEach((filter, index) => {
    let keyPrefix = `filters[${index}].`;
    queryParams.append(keyPrefix + 'fieldName', filter.fieldName);
    queryParams.append(keyPrefix + 'fieldType', filter.fieldType.toString());
    if (
      filter.value !== undefined &&
      filter.value !== null &&
      !Array.isArray(filter.value)
    ) {
      queryParams.append(keyPrefix + 'value', filter.value);
    } else if (Array.isArray(filter.value)) {
      queryParams.append(keyPrefix + 'value', filter.value.join(','));
    }
  });
  return queryParams;
};
