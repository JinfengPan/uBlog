import {combineReducers} from 'redux';
import courses from './courseReducer';
import ajaxCallsInProgress from './ajaxStatusReducer';
import authors from './authorReducer';
import {reducer as FormReducer} from 'redux-form';

//reducer名称需要与与global state中某个state的名称一致
const rootReducer = combineReducers({
  courses,
  ajaxCallsInProgress,
  authors,
  form: formReducer
});

export default rootReducer;
