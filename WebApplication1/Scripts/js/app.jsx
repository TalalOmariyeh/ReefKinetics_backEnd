var hello = React.createClass({
    render: function () {
        return (
            <div>hello my name/div>
        );
    }

});
React.render(<hello />, document.getElementById("reactdiv"))