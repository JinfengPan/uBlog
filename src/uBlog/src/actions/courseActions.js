import * as types from './actionTypes';
import courseApi from '../api/mockCourseApi';
import {beginAjaxCall, ajaxCallError} from './ajaxStatusAction';

export function loadCoursesSuccess(courses){
  return {type: types.LOAD_COURSES_SUCCESS, courses};
}

export function createCourseSuccess(course){
  return{type: types.CREATE_COURSE_SUCCESS, course};
}

export function updateCourseSuccess(course){
  return{type: types.UPDATE_COURSE_SUCCESS, course};
}

//异步请求成功后再dispath一个action, 在action中包含响应结果
export function loadCourses(){
  return function(dispatch){

    dispatch(beginAjaxCall());

    return courseApi.getAllCourses().then(courses => {
      dispatch(loadCoursesSuccess(courses));
    }).catch(error => {
      throw(error);
    });
  };
}


export function saveCourse(course){
  return function(dispatch, getState){

    dispatch(beginAjaxCall());

    return courseApi.saveCourse(course).then(course =>{
      course.id
        ? dispatch(updateCourseSuccess(course))
        : dispatch(createCourseSuccess(course));

    }).catch(error =>{
      dispatch(ajaxCallError(error));
      throw(error);
    });
  };
}
