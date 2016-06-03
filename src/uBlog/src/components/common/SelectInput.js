import React, {PropTypes} from 'react';

const SelectInput = ({name, label, onChange, defaultOption, value, error, options}) =>{
  return(
    <div className="form-group">
      <label htmlFor={name}>{label}</label>
      <div className="field">
        <select
          name={name}
          value={value}
          onChange={onChange}
          className="form-control">
          {options.map((option) =>{
            
          })}
        </select>
      </div>
    </div>

  );
};
