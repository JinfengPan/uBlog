import * as types from './actionTypes';
import courseApi from '../api/mockCourseApi';

export function loadCoursesSuccess(courses){
  return {type: types.LOAD_COURSES_SUCCESS, courses}
}

//异步请求成功后再dispath一个action, 在action中包含响应结果
export function loadCourses(){
  return function(dispatch){
    return courseApi.getAllCourses().then(courses => {
      dispatch(loadCoursesSuccess(courses));
    }).catch(error => {
      throw(error);
    });
  }
}
