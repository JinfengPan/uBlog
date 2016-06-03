import React, {PropTypes} from 'react';
import {connect} from 'react-redux';
import {bindActionCreators} from 'redux';
import * as courseActions from '../../actions/courseActions';
import CourseList from './CourseList';

class CoursesPage extends React.Component {
  constructor(props, context){
    super(props, context);
  }

  courseRow(course, index){
    return <div key={index}>{course.title}</div>;
  }

  render() {
    const {courses} =  this.props;
    return (
      <div>
        <h1>Courses</h1>
        <CourseList courses = {courses}/>
      </div>
    );
  }
}

CoursesPage.propTypes = {
  courses:PropTypes.array.isRequired
}

function mapStateToProps(state, ownProps){
  return{
    courses: state.courses
  };
}

// function mapDispatchToProps(dispatch){
//   return{
//     actions: bindActionCreators(courseactions, dispatch)
//   }
// }

export default connect(mapStateToProps,null)(CoursesPage);
